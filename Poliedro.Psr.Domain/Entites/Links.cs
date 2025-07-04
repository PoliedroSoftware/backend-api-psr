namespace Poliedro.Psr.Domain.Entites;

public class Links
{
    public Href Self { get; set; }

    public class Href
    {
        public string Hrefs { get; set; }
    }
}
