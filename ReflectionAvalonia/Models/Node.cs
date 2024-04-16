using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ReflectionAvalonia.Models
{
    public class Node
    {
        public ObservableCollection<Node>? SubNodes { get; }
        public string Title { get; }
        public object Value { get; }

        public Node(string title,object value)
        {
            Title = title;
            Value = value;
        }

        public Node(string title,object value, ObservableCollection<Node> subNodes)
        {
            Title = title;
            Value = value;
            SubNodes = subNodes;
        }
    }

    public class AnytypePresentationModel
{
    private readonly PresentationComponent? presentationComponent;
    private readonly ObservableCollection<Node> treeView;

    public AnytypePresentationModel(object presentationObject)
    {
        var factory = new PresentationModelFactory();

        if(presentationObject is User.User user)
        {
            presentationComponent = factory.Create(user);
        }

        if (presentationComponent != null)
        {
            PresentationComponentToTreeViewAdapter toTreeViewAdapter = 
                new(presentationComponent);

            treeView = toTreeViewAdapter.Adapt();
        }
        else
        {
            treeView = [];
        }
    }

    public ObservableCollection<Node> GetPresentation()
    {
        return treeView;
    }
}


    public abstract class PresentationComponent
    {
        private List<PresentationComponent> children = [];
        public List<PresentationComponent> Childern
        {
            get => children;
            private set => children = value;
        }

        public string Name { get; set; }
        public object? Value { get; set; }

        public PresentationComponent(string name, object value)
        {
            Name = name;
            Value = value;
        }

        protected PresentationComponent(string name)
        {
            Name = name;
        }

        public abstract void Add(PresentationComponent component);
        public abstract void Remove(PresentationComponent component);
    }

    public class PresentationComponentToTreeViewAdapter(PresentationComponent presentationComponent)
    {
        private readonly PresentationComponent presentationComponent = presentationComponent;

        private static ObservableCollection<Node> AdaptRecursivly(
            ObservableCollection<Node> currentLevel,
            PresentationComponent currentComponent)
        {
            if(currentComponent is PresentationLeaf leaf)
            {
                currentLevel.Add(new Node(leaf.Name,leaf.Value));
            }

            if(currentComponent is PresentationComposite composite)
            {
                foreach(var child in composite.Childern)
                {
                    if(child is PresentationLeaf nestedLeaf)
                    {
                        currentLevel.Add(new Node(nestedLeaf.Name, nestedLeaf.Value));
                    }
                    else
                    {
                        currentLevel.Add(new Node(child.Name,
                            child.Value,
                            AdaptRecursivly([], child)));
                    }
                }
            }

            return currentLevel;
        }

        public ObservableCollection<Node> Adapt()
        {
            ObservableCollection<Node> treeview = [];
            AdaptRecursivly(treeview, presentationComponent);
            return treeview;
        }
    }

    public class PresentationComposite(string name, object value) : PresentationComponent(name, value)
    {
        public override void Add(PresentationComponent component)
        {
            Childern.Add(component);
        }
        public override void Remove(PresentationComponent component) 
        {
            Childern.Remove(component);
        }
    }

    public class PresentationLeaf(string name, object value) : PresentationComponent(name,value)
    {
        public override void Add(PresentationComponent component)
        {
        }
        public override void Remove(PresentationComponent component)
        {
        }
    }


    
}
