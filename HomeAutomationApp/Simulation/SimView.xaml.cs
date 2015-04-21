using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace HomeAutomationApp
{
public partial class SimView : ContentPage
{

	private readonly Simulator simulator;
	ObservableCollection<string> Items;

	bool sim = false;

	public SimView(Simulator sim)
	{
		simulator = sim;
		Items = new ObservableCollection<string>();

		InitializeComponent();

		SimList.ItemsSource = Items;

		Device.StartTimer(TimeSpan.FromSeconds(5), StartSimulation);
	}

	bool StartSimulation()
	{
		if(!sim)
		{
			simulator.StartSimulation(Items);
			sim = true;
		}
		return false;
	}

}
}

