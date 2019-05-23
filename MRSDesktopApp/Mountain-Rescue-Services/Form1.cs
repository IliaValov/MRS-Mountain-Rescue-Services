using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mountain_Rescue_Services
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider provider;

        public Form1(IServiceProvider provider)
        {
            InitializeComponent();
            this.provider = provider;
        }

        private void GMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            gMapControl1.Position = new PointLatLng(42.666551, 23.350466);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;
        }

        private void UserBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
