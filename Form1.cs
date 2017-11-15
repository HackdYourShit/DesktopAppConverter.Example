using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather.WinForm
{
    public partial class Form1 : Form
    {
        public List<Weather> forecastList;
        public string[] Cities = { "Chennai", "Mumbai", "Delhi" };

        public Form1()
        {
            InitializeComponent();

            cbCities.SelectedIndex = 0;
        }

        private void cbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            forecastList = WeatherServiceManager.Instance.GetForecast(Cities[cbCities.SelectedIndex]);
            updateTemperature();
        }

        void updateTemperature()
        {
            lblHigh.Text = ": " + (forecastList.Count > 0 ? forecastList[0].High : "0") + "°C";
            lblLow.Text = ": " + (forecastList.Count > 0 ? forecastList[0].Low : "0") + "°C";
        }
    }
}
