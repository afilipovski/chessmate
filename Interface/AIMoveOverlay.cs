using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Interface
{
	internal class AIMoveOverlay
	{
		public void Draw(Graphics g)
		{
			SolidBrush sb = new SolidBrush(Color.Black);
			SolidBrush sbs = new SolidBrush(Color.White);

			int boxOffsetX = Board.OFFSET + 3 * Board.HEIGHT;
			int boxOffsetY = (int)(3.5 * Board.HEIGHT);

			int boxWidth = 2 * Board.HEIGHT;
			int boxHeight = Board.HEIGHT;

			float fontSize = (float)(boxHeight * 0.3);
			int textOffsetX = boxOffsetX;
			int textOffsetY = (int)(boxOffsetY + (boxHeight - fontSize) / 2);

			Font f = new Font("Arial", fontSize);

			//g.FillRectangle(b, X * Board.HEIGHT + Board.OFFSET, Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
			//g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), X * Board.HEIGHT + Board.OFFSET, Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);

			g.FillRectangle(sb, boxOffsetX, boxOffsetY, boxWidth, boxHeight);
			g.DrawString("AI turn...", f, sbs, textOffsetX, textOffsetY - 10);
		}
	}
}
