
using Poliedro.Psr.Domain.Ports;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace Poliedro.Psr.Application.Services;

public class QRCodeGeneratorService : IGenerarCodeQr
{
    public void Excute(string texto, string rutaArchivo)
    {
        //using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
        //using QRCode qrCode = new QRCode(qrCodeData);
        //using Bitmap qrCodeImage = qrCode.GetGraphic(20);
        //qrCodeImage.Save(rutaArchivo, ImageFormat.Png);
    }
}
