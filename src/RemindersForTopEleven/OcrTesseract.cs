using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace RemindersForTopEleven
{
    class OcrTesseract
    {
        internal string WorkDirectory { get; set; }

        internal string GetTextImage(string fileName, string language = "eng")
            => GetText(new Bitmap($@"{fileName}"), language);

        private string GetText(Bitmap imgsource, string language)
        {
            string ocrtext = string.Empty;

            try
            {
                using (TesseractEngine engine = new TesseractEngine($@"{WorkDirectory}tessdata", language, EngineMode.TesseractOnly))
                {
                    using (Pix img = PixConverter.ToPix(imgsource))
                    {
                        using (Page page = engine.Process(img))
                        {
                            ocrtext = page.GetText();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return ocrtext;
        }
    }
}
