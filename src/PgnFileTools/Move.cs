﻿using System;

namespace PgnFileTools
{
    public class Move
    {
        public File DestinationFile { get; set; }
        public Row DestinationRow { get; set; }
        public PieceType PieceType { get; set; }
        public bool HasError { get; set; }
    }
}