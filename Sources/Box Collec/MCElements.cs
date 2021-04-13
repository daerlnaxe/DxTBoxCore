using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DxTBoxCore.Box_Collec
{
    /// <summary>
    /// Model for elements collected, with add/remove interaction
    /// </summary>
    /// <remarks>
    /// Work with strings
    /// </remarks>
    public class MCElements
    {
        public string Message { get; set; }

        public List<string> AvailableElements;

        public ObservableCollection<string> ChosenElements { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> DiscardedElements { get; set; } = new ObservableCollection<string>();

        public MCElements(params string[] elements)
        {
            AvailableElements = elements.ToList();
            // Assign elements to Chosen
            ChosenElements = new ObservableCollection<string>(elements);
            
        }

        public MCElements(List<string> elements)
        {
            AvailableElements = elements;
            ChooseAll();
        }

        public void RemoveAllChosen()
        {
            ChosenElements.Clear();
            DiscardedElements.Clear();

            foreach (var e in AvailableElements)
                DiscardedElements.Add(e);
        }

        public void ChooseAll()
        {
            ChosenElements.Clear();
            DiscardedElements.Clear();

            foreach (var e in AvailableElements)
                ChosenElements.Add(e);
        }


    }
}
