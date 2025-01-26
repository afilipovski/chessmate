﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Domain.Pieces;

namespace ChessMate.Presentation.GraphicsRenderers.Renderers
{
    public class PieceRenderer : IShapeRenderer<Piece>
    {
        private readonly Dictionary<string, Bitmap> ImageByName = new Dictionary<string, Bitmap>
        {
            { "black-king", Properties.Resources.b_king_png_shadow_1024px },
            { "white-king", Properties.Resources.w_king_png_shadow_1024px },
            { "black-queen", Properties.Resources.b_queen_png_shadow_1024px },
            { "white-queen", Properties.Resources.w_queen_png_shadow_1024px },
            { "black-pawn", Properties.Resources.b_pawn_png_shadow_1024px },
            { "white-pawn", Properties.Resources.w_pawn_png_shadow_1024px },
            { "black-rook", Properties.Resources.b_rook_png_shadow_1024px },
            { "white-rook", Properties.Resources.w_rook_png_shadow_1024px },
            { "black-bishop", Properties.Resources.b_bishop_png_shadow_1024px },
            { "white-bishop", Properties.Resources.w_bishop_png_shadow_1024px },
            { "black-knight", Properties.Resources.b_knight_png_shadow_1024px },
            { "white-knight", Properties.Resources.w_knight_png_shadow_1024px },
        };

        public void Draw(Graphics graphics, Piece shape)
        {
            Bitmap bitmap = ImageByName[shape.NameColor()];
            graphics.DrawImage(bitmap, shape.Position.X * Board.TILE_SIDE + Board.OFFSET_X, shape.Position.Y * Board.TILE_SIDE + Board.OFFSET_Y, Board.TILE_SIDE, Board.TILE_SIDE);
        }
    }
}
