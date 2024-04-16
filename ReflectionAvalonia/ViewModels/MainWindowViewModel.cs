using ReactiveUI;
using ReflectionAvalonia.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ReflectionAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AnytypePresentationViewModel> anytypePresentationViewModels 
            = [];

        public MainWindowViewModel(List<AnytypePresentationModel> models)
        {
            foreach (var model in models)
            {
                anytypePresentationViewModels.Add(new AnytypePresentationViewModel(model));
            }

            DrawingContent = anytypePresentationViewModels[1];
        }

        private object drawingContent;
        public object DrawingContent {

            get => drawingContent;
            set => this.RaiseAndSetIfChanged(ref drawingContent, value);
        }
    }

    public class AnytypePresentationViewModel : ViewModelBase
    {
        private readonly AnytypePresentationModel model;
        public AnytypePresentationViewModel(AnytypePresentationModel presentationModel)
        {
            model = presentationModel;
            TreeView =  model.GetPresentation();
        }

        private ObservableCollection<Node> treeView;
        public ObservableCollection<Node> TreeView
        {
            get => treeView;
            set => this.RaiseAndSetIfChanged(ref treeView, value);
        }
    }
}
