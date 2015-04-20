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


	public SimView(Simulator sim)
	{
		simulator = sim;

		InitializeComponent();

		SimList.ItemsSource = Items;

		StartSimulation();
	}

	void StartSimulation()
	{
		simulator.StartSimulation(Items);
	}

}
}

