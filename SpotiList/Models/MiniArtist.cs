namespace SpotiList.Models
{
    public class MiniArtist
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MiniArtist)
                return Id == ((MiniArtist)obj).Id;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}