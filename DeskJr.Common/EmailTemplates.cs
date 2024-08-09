namespace DeskJr.Common
{
    public static class EmailTemplates
    {
        public static string LeaveRequestNotificationTemplate = @"
            <!DOCTYPE html>
            <html lang='tr'>
            <head>
                <style>
                    @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap');
                    body { font-family: 'Roboto', sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }
                    .container { max-width: 600px; margin: 20px auto; padding: 0; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); overflow: hidden; }
                    .header { background-color: #002855; color: #ffffff; padding: 20px; text-align: center; }
                    .content { padding: 20px; color: #333333; position: relative; }
                    .content img { position: absolute; bottom: 20px; right: 1px; width: 100px; height: auto; opacity: 0.9; }
                    .footer { margin-top: 20px; text-align: center; color: #888888; font-size: 12px; padding: 10px 20px; background-color: #f4f4f4; }
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>DESKJR - İzin Talebi Bildirimi</h2>
                    </div>
                    <div class='content'>
                        <p>Merhaba {TeamLeaderName},</p>
                        <p>Çalışan {EmployeeName} aşağıdaki tarihler için izin talebinde bulunmuştur:</p>
                        <ul>
                            <li><strong>Başlangıç Tarihi:</strong> {StartDate}</li>
                            <li><strong>Bitiş Tarihi:</strong> {EndDate}</li>
                        </ul>
                        <p>Lütfen talebi inceleyip onaylayın veya reddedin.</p>
                        <p>Teşekkürler,</p>
                        <p><strong>DESKJR</strong></p>
                        <img src='https://forte.com.tr/assets/imgs/fav/logo.png' alt='Forte Bilgi İletişim Teknolojileri ve Savunma Sanayi A.Ş'
                    </div>
                    <div class='footer'>
                        <p>Bu bir otomatik mesajdır, lütfen yanıtlamayın.</p>
                    </div>
                </div>
            </body>
            </html>";

        public static string LeaveRequestResponseTemplate = @"
        <!DOCTYPE html>
        <html lang='tr'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <style>
                @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap');
                body { font-family: 'Roboto', sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }
                .container { max-width: 600px; margin: 20px auto; padding: 20px; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); }
                .approved { background-color: #28A745; color: #ffffff; padding: 20px; border-radius: 12px 12px 0 0; text-align: center; }
                .reject { background-color: #DC3545; color: #ffffff; padding: 20px; border-radius: 12px 12px 0 0; text-align: center; }
                .content { padding: 20px; color: #333333; }
                .footer { margin-top: 20px; text-align: center; color: #888888; font-size: 12px; }
                .button { display: inline-block; padding: 12px 24px; margin-top: 20px; background-color: #28A745; color: #ffffff; text-decoration: none; border-radius: 8px; font-weight: 500; transition: background-color 0.3s; }
                .button.reject { background-color: #DC3545; }
                .button:hover { opacity: 0.9; }
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='{ApprovalStatusClass}'>
                    <h2>DESKJR - İzin Talebi {ApprovalStatus}</h2>
                </div>
                <div class='content'>
                    <p>Merhaba {EmployeeName},</p>
                    <p>İzin talebiniz {ApprovalStatus}.</p>
                    <ul>
                        <li><strong>Başlangıç Tarihi:</strong> {StartDate}</li>
                        <li><strong>Bitiş Tarihi:</strong> {EndDate}</li>
                    </ul>
                    <p>Teşekkürler,</p>
                    <p><strong>DESKJR</strong></p>
                </div>
                <div class='footer'>
                    <p>Bu bir otomatik mesajdır, lütfen yanıtlamayın.</p>
                </div>
            </div>
        </body>
        </html>";
    }
}

