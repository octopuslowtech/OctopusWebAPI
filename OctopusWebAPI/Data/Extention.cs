using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace OctopusWebAPI.Data
{
    public class Extention
    {
        public static async Task<bool> WriteFile(IFormFile file)
        {
            try
            {
                 string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\backup");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var stream = new FileStream(Path.Combine(path, file.FileName.Replace(" ", "")), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception e)
            {
                
            }

            return false;
        }

         


        public static async Task<Point> CaptchaDemo(string base64String)
        {
            try
            {
                var Screen = Base64StringToBitmap(base64String);
                if (Screen == null)
                    return Point.Empty;
                var main = Screen.ToImage<Bgr, byte>();
                var img = Screen.ToImage<Gray, byte>();
                var blur = new Image<Gray, byte>(img.Width, img.Height);
                CvInvoke.GaussianBlur(img, blur, new System.Drawing.Size(3, 3), 0);
                var canny = new Image<Gray, byte>(img.Width, img.Height);
                CvInvoke.Canny(blur, canny, 700, 400);
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hier = new Mat();
                CvInvoke.FindContours(canny, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                for (int i = 0; i < contours.Size; i++)
                {
                    var contour = contours[i];
                    VectorOfPoint approx = new VectorOfPoint();
                    double perimeter = CvInvoke.ArcLength(contour, true);
                    CvInvoke.ApproxPolyDP(contours[i], approx, 0.04 * perimeter, true);
                    if (approx.Size >= 4 && approx.Size <= 8 && perimeter > 200)
                    {
                        CvInvoke.DrawContours(main, contours, i, new MCvScalar(0, 0, 255));
                        var momment = CvInvoke.Moments(contour);
                        int cX = (int)Math.Round(momment.M10 / momment.M00);
                        int cY = (int)Math.Round(momment.M01 / momment.M00);
                        if (cX > 72)
                        {
                            CvInvoke.PutText(main, Math.Round(perimeter).ToString(), new Point(cX, cY), Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1.0, new MCvScalar(0, 0, 255), 2);
                            return new Point(cX, cY);
                        }
                    }
                }
            }
            catch { }
            

            return new Point();
        }
        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "Oct@pusr@nd3m";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        public static async Task<string> GetTWOFACode(string twofa)
        {
            var secretKey = Base32Encoding.ToBytes(twofa.Replace(" ",""));
            var totp = new Totp(secretKey);
            var otp = totp.ComputeTotp();
            return otp.ToString();
        }
         public static Bitmap Base64StringToBitmap(string base64String)
        {
            byte[] buffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(buffer);
            memoryStream.Position = 0L;
            Bitmap result = (Bitmap)Image.FromStream(memoryStream);
            memoryStream.Close();
            return result;
        }
    }
}
