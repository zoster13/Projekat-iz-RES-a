namespace CommonLibrary
{
    public interface IDumpingBuffer
    {
        void WriteToDumpingBuffer(Codes code, float value);
    }
}
