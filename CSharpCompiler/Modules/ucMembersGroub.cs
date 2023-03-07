using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpCompiler.Modules
{
    public partial class ucMembersGroub : UserControl
    {
        public ucMembersGroub()
        {
            InitializeComponent();
        }

        private void ucMembersGroub_Paint(object sender, PaintEventArgs e)
        {
        }

        private void MembersGroub_Paint(object sender, PaintEventArgs e)
        {

            string Names =
                "\n\n\nCopyright "+Char.ConvertFromUtf32(169)+
                " 2021 All Rights Reserved By Eng\\\n\n\n \t\t\t Tariq Mohammed Al-Sultan\n" 
                ;
           

            Graphics graphics = e.Graphics;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font font = new Font("MidnightSnack BB", 15, FontStyle.Regular, GraphicsUnit.Point);
            PointF startpoint = new PointF(6, 6);
            SizeF textsize = graphics.MeasureString(Names, font);
            PointF endpoint = new PointF(textsize.Width, textsize.Height);
            LinearGradientBrush brush = new LinearGradientBrush(startpoint, endpoint, Color.GreenYellow, Color.Gainsboro);
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = new Color[]
           {
                Color.Gold,
                Color.Goldenrod,
                Color.LightGoldenrodYellow,
                Color.DarkGoldenrod,
                Color.PaleGoldenrod
           };
            colorBlend.Positions = new float[]
            {
                0f,3/9f,6/9f,8/9f,1f
            };
            brush.InterpolationColors = colorBlend;
            graphics.DrawString(Names, font, brush, 5, 37);
            // this.Dock = DockStyle.Right;
            // this.Width = (int)textsize.Width + 15;
        }
    }
}
