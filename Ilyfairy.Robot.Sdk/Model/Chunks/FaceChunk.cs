namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class FaceChunk : MessageChunk
    {
        public int Id { get; set; }
        //public FaceChunk()
        //{

        //}

        public FaceChunk(int id)
        {
            OriginText = $"[CQ:face,id={id}]";
            this.Type = CQCode.face;
            this.Id = id;
        }

    }
}