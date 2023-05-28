using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServerApp
{
    class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            mainForm.DefaultSelected += (sender, e) => StartDefaultServer();
            mainForm.CodingSelected += (sender, e) => StartCodingServer();
            mainForm.CustomSelected += (sender, e) => StartCustomServer(mainForm.GetCustomCode());
            mainForm.StartServerClicked += (sender, e) => StartCustomServer(mainForm.GetCustomCode());

            Application.Run(mainForm);
        }

        static void StartDefaultServer()
        {
            var server = new SimpleWebServer();
            server.Response += (sender, e) =>
            {
                e.ResponseString = GetDefaultResponseString();
            };
            server.Start();
        }

        static void StartCodingServer()
        {
            var server = new SimpleWebServer();
            server.Response += (sender, e) =>
            {
                e.ResponseString = GetCodingResponseString();
            };
            server.Start();
        }

        static void StartCustomServer(string customCode)
        {
            var server = new SimpleWebServer();
            server.Response += (sender, e) =>
            {
                e.ResponseString = GetCustomResponseString(customCode);
            };
            server.Start();
        }

        static string GetDefaultResponseString()
        {
            return "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\"><title>HH Beta</title><style>body{background-image:url(https://upload.wikimedia.org/wikipedia/commons/c/cc/Digital_rain_animation_medium_letters_shine.gif);background-repeat:no-repeat;background-size:cover;font-family:Lato,sans-serif}.overlay{height:100%;width:0;position:fixed;z-index:1;top:0;left:0;background-color:rgba(0,0,0,.9);overflow-x:hidden;transition:.5s}.overlay-content{position:relative;top:25%;width:100%;text-align:center;margin-top:30px}.overlay a{padding:8px;text-decoration:none;font-size:36px;color:#818181;display:block;transition:.3s}.overlay a:focus,.overlay a:hover{color:#f1f1f1}.overlay .closebtn{position:absolute;top:20px;right:45px;font-size:60px}@media screen and (max-height:450px){.overlay a{font-size:20px}.overlay .closebtn{font-size:40px;top:15px;right:35px}}</style></head><body><div id=\"myNav\" class=\"overlay\"><a href=\"javascript:void(0)\" class=\"closebtn\" onclick=\"closeNav()\">&times;</a><div class=\"overlay-content\"><a href=\"#\" onclick='showSection(\"home-section\")'>Home</a><a href=\"#\" onclick='showSection(\"credits-section\")'>Credits</a><a href=\"#\" onclick='showSection(\"members-section\")'>Members</a><a href=\"https://lesliek12.my.canva.site/#page-c\">Support Us</a><a href=\"#\">New Page</a></div></div><div id=\"home-section\" style=\"display:block\"><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><h2 style=\"color:#0f0\">HackerHawks#21491 Beta Site</h2></div><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><p style=\"color:#0f0\">This is a Beta of the Hackerhawks#21491 Site. If you want the main one, this is not what you are looking for.</p></div></div><div id=\"members-section\" style=\"display:none\"><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><h2 style=\"color:#0f0\">HackerHawks#21491 Team Members</h2></div><div style=\"display:flex;justify-content:center;bottom:10%\"><p style=\"color:#0f0\"><br>Joshua Thayer<br>Brenden Bunker<br>Drake Carian<br>Eliseo Brown<br>Jasmen Thompson<br>Koty Lewis<br>(List is not finished)</p></div></div><div id=\"credits-section\" style=\"display:none\"><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><h2 style=\"color:#0f0\">Hacker Hawks Beta Site</h2></div><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><p style=\"color:#0f0\">The Main Hackerhawks#21491 Site and this Beta site are both maintained and hosted by Joshua Thayer.</p></div><div style=\"display:flex;justify-content:center;align-items:center;height:10vh\"><p style=\"color:#0f0\">Offline robot code editor download:</p><a href=\"https://hackerhawks.github.io/named%20motors%20offline%20blocks%20editor.zip\" download=\"offlineeditor\"><img src=\"https://hackerhawks.github.io/favicon.ico\" alt=\"HHofflineeditor\" width=\"104\" height=\"104\"></a></div></div><div style=\"position:absolute;top:0;right:0\"><span style=\"color:#0f0;font-size:300%;cursor:pointer\" onclick=\"openNav()\">&#9776;</span></div><script>function openNav(){document.getElementById(\"myNav\").style.width=\"100%\"}function closeNav(){document.getElementById(\"myNav\").style.width=\"0%\"}function showSection(e){document.querySelectorAll(\"[id$='-section']\").forEach(function(e){e.style.display=\"none\"}),document.getElementById(e).style.display=\"block\",closeNav()}</script></body></html>";

        }

        static string GetCodingResponseString()
        {
            return "<!DOCTYPE html><html><head><title>HTML Text Editor</title><style>.editor{width:100%;height:400px;border:1px solid #ccc;padding:10px}</style></head><body><div class=\"editor\" contenteditable=\"true\"></div><button id=\"saveBtn\">Save</button><label for=\"loadBtn\">Load</label><input type=\"file\" id=\"loadBtn\" style=\"display:none\" accept=\".html\"><script>var editor=document.querySelector(\".editor\"),saveBtn=document.getElementById(\"saveBtn\"),loadBtn=document.getElementById(\"loadBtn\");editor.addEventListener(\"input\",function(){var e=editor.innerHTML;console.log(e)}),saveBtn.addEventListener(\"click\",function(){var e=editor.innerHTML,t=prompt(\"Enter file name\",\"editor.html\");if(t){var n=new Blob([e],{type:\"text/html\"}),r=document.createElement(\"a\");r.href=URL.createObjectURL(n),r.download=t,r.click()}}),loadBtn.addEventListener(\"change\",function(e){var t=e.target.files[0],n=new FileReader;n.onload=function(){var e=n.result;editor.innerHTML=e},n.readAsText(t)})</script></body></html>";

        }

        static string GetCustomResponseString(string customCode)
        {
            return  customCode;
        }

        static Icon GetEmbeddedIcon(string resourceName)
        {
            using (Stream iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (iconStream != null)
                {
                    return new Icon(iconStream);
                }
            }

            return null;
        }
    }

    class MainForm : Form
    {
        private Button defaultButton;
        private Button codingButton;
        private TextBox customCodeTextBox;
        private Button customButton;
        private Button startButton;

        public event EventHandler DefaultSelected;
        public event EventHandler CodingSelected;
        public event EventHandler CustomSelected;
        public event EventHandler StartServerClicked;

        private NotifyIcon notifyIcon;

        public MainForm()
        {
            InitializeComponent();

            ContextMenu contextMenu = new ContextMenu();
            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += (sender, e) => Application.Exit();
            contextMenu.MenuItems.Add(exitMenuItem);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        private void InitializeComponent()
        {
            this.defaultButton = new System.Windows.Forms.Button();
            this.codingButton = new System.Windows.Forms.Button();
            this.customCodeTextBox = new System.Windows.Forms.TextBox();
            this.customButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.defaultButton.Location = new System.Drawing.Point(12, 12);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(150, 30);
            this.defaultButton.TabIndex = 0;
            this.defaultButton.Text = "Default Page";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            this.codingButton.Location = new System.Drawing.Point(12, 48);
            this.codingButton.Name = "codingButton";
            this.codingButton.Size = new System.Drawing.Size(150, 30);
            this.codingButton.TabIndex = 1;
            this.codingButton.Text = "Coding Page";
            this.codingButton.UseVisualStyleBackColor = true;
            this.codingButton.Click += new System.EventHandler(this.codingButton_Click);
            this.customCodeTextBox.Location = new System.Drawing.Point(12, 84);
            this.customCodeTextBox.Multiline = true;
            this.customCodeTextBox.Name = "customCodeTextBox";
            this.customCodeTextBox.Size = new System.Drawing.Size(300, 200);
            this.customCodeTextBox.TabIndex = 2; 
            this.customButton.Location = new System.Drawing.Point(12, 290);
            this.customButton.Name = "customButton";
            this.customButton.Size = new System.Drawing.Size(150, 30);
            this.customButton.TabIndex = 3;
            this.customButton.Text = "Custom Page";
            this.customButton.UseVisualStyleBackColor = true;
            this.customButton.Click += new System.EventHandler(this.customButton_Click);
            this.startButton.Location = new System.Drawing.Point(168, 290);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(144, 30);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start Server";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            this.ClientSize = new System.Drawing.Size(324, 332);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.customButton);
            this.Controls.Add(this.customCodeTextBox);
            this.Controls.Add(this.codingButton);
            this.Controls.Add(this.defaultButton);
            this.Name = "MainForm";
            this.Text = "hhwebserver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void defaultButton_Click(object sender, EventArgs e)
        {
            DefaultSelected?.Invoke(this, EventArgs.Empty);
        }
        private void codingButton_Click(object sender, EventArgs e)
        {
            CodingSelected?.Invoke(this, EventArgs.Empty);
        }

        private void customButton_Click(object sender, EventArgs e)
        {
            CustomSelected?.Invoke(this, EventArgs.Empty);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartServerClicked?.Invoke(this, EventArgs.Empty);
        }
        public string GetCustomCode()
        {
            return customCodeTextBox.Text;
        }
    }

    class SimpleWebServer
    {
        private HttpListener listener;
        private bool isRunning;

        public event EventHandler<WebResponseEventArgs> Response;

        public SimpleWebServer()
        {
            listener = new HttpListener();
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                Task.Run(() => StartListening());
            }
        }

        private void StartListening()
        {
            listener.Prefixes.Add("http://127.0.0.1:3012/");
            listener.Start();
            while (isRunning)
            {
                try
                {
                    var context = listener.GetContext();
                    HandleRequest(context);
                }
                catch (HttpListenerException)
                {
                }
            }
            listener.Stop();
        }

        private void HandleRequest(HttpListenerContext context)
        {
            var response = new WebResponseEventArgs();
            Response?.Invoke(this, response);

            string responseString = response.ResponseString;
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            using (Stream outputStream = context.Response.OutputStream)
            {
                outputStream.Write(buffer, 0, buffer.Length);
            }

            context.Response.Close();
        }
    }

    class WebResponseEventArgs : EventArgs
    {
        public string ResponseString { get; set; }
    }
}
