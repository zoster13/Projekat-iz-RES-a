//-----------------------------------------------------------------------
// <copyright file="IDumpingBuffer.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

namespace CommonLibrary
{
    public interface IDumpingBuffer
    {
        /// <summary>
        /// Function for sending Code and Value to DumpingBuffer
        /// </summary>
        /// <param name="code"> Code </param>
        /// <param name="value"> Value </param>
        void WriteToDumpingBuffer(Codes code, float value);
    }
}
