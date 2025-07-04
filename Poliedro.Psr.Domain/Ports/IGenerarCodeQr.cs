namespace Poliedro.Psr.Domain.Ports;

public interface IGenerarCodeQr
{
    void Excute(string texto, string rutaArchivo);
}
