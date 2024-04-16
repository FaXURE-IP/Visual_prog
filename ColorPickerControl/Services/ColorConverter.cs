using ColorPickerControl.ViewModels;
using System;
using System.Drawing;
using System.Text;

namespace ColorPickerControl.Services
{
    public static class ColorConverter
    {
        public static string RGBToHexConvert(RGBViewModel color)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('#').Append(string.Format("{0:X2}{1:X2}{2:X2}",
                color.Red, color.Green, color.Blue));

            return sb.ToString();
        }

        public static void HexToRGBConvert(string color,RGBViewModel viewModel)
        {
            var rgb = ColorTranslator.FromHtml(color);

            viewModel.Red = rgb.R;
            viewModel.Green = rgb.G;
            viewModel.Blue = rgb.B;
        }
    }
}
