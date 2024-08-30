using System.Drawing;
using QRCoder;

namespace Tokiwi {
    public class CodeQR {
        private Config config;

        private string identifiant;

        private byte[] qrCodeImage;
        
        public CodeQR(Config config, string identifiant) {
            this.config = config;
            this.identifiant = identifiant;
        }

        public void generateQRCode() {
            QRCodeGenerator qRGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRGenerator.CreateQrCode($"{config.siteQRCodes}/{this.identifiant}", QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qRCodeData);
            ImageConverter converter = new ImageConverter();
            this.qrCodeImage = qrCode.GetGraphic(20);
        }

        public byte[] getQRCode() {
            return this.qrCodeImage;
        }
    }
}
