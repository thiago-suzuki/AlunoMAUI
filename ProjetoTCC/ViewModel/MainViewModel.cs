//using Android.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjetoTCC.ViewModel;

public partial class MainViewModel : ObservableObject
{
    //Monkey monkey;
    IConnectivity connectivity;
    //IToast toast;
    public MainViewModel(IConnectivity connectivity)//, IToast toast)
    {
        //monkey = new Monkey { Name = "Mooch" };
        this.connectivity = connectivity;
        //this.toast = toast;
    }

    [ObservableProperty]
    int count;
    

    [RelayCommand]
    void IncrementCount()
    {
        Count += 1;
    }

    [RelayCommand]
    async Task Navigate()
    {
        NetworkAccess accessType = connectivity.NetworkAccess;

        var evento = await Shell.Current.DisplayActionSheet("Eventos", null, "Fechar", "Abrir no Aplicativo", "Abrir no Navegador");

        if (accessType == NetworkAccess.Internet)
        {
            if (evento == "Abrir no Aplicativo")
            {
                try
                {
                    await Shell.Current.GoToAsync(nameof(DetailPage));
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", ex.ToString(), "ok");
                    //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    //string text = "This is a Toast";

                    //var toast = Toast.MakeText(context:null,text, ToastLength.Short);

                    //toast.Show();
                }
            }
            if (evento == "Abrir no Navegador")
            {
                try
                {
                    Uri uri = new Uri("https://portalaluno.unisanta.br/");
                    await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", ex.ToString(), "ok");
                }
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Conexão", "Sem Conexão a Internet", "ok");
        }
    }

    [RelayCommand]
    async Task NavigateCovid()
    {
        NetworkAccess accessType = connectivity.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            await Shell.Current.GoToAsync(nameof(CovidPage));
        }
        else
        {
            await Shell.Current.DisplayAlert("Conexão", "Sem Conexão a Internet", "ok");
        }
    }

    [RelayCommand]
    async Task CheckInternet()
    {
        NetworkAccess accessType = connectivity.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            //toast.MakeToast("You Have Internet!");
            await Shell.Current.DisplayAlert("Conexão", "Conectado a internet", "ok");
        }
        else
        {
            await Shell.Current.DisplayAlert("Conexão", "Sem Conexão a Internet", "ok");
        }
    }

}
