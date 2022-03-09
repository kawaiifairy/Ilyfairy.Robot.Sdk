namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class FaceChunk : MessageChunk
    {
        public int Face { get; set; }
        internal FaceChunk()
        {

        }

        public FaceChunk(int id)
        {
            OriginText = $"[CQ:face,id={id}]";
            this.Type = CQCode.face;
            this.Face = id;
        }

    }
}