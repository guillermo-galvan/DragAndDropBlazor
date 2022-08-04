using DragAndDrop.Models;
using Microsoft.AspNetCore.Components;

namespace DragAndDrop.Shared
{
    public partial class DragDrop<TItem>
    {
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }

        [Parameter]
        public RenderFragment<TItem>? ItemRender { get; set; }

        readonly List<DragDropModel<TItem>> _newPosition = new();
        DragDropModel<TItem>? _selected;
        protected override void OnParametersSet()
        {
            if (Items != null)
            {
                _newPosition.Clear();
                int count = 1;
                foreach (var item in Items)
                {
                    _newPosition.Add(new DragDropModel<TItem> { Index = count++, Item = item });
                }
            }
        }

        void OnClick(DragDropModel<TItem> item)
        {
            _newPosition.FindAll(x => x != item).ForEach(x => x.IsActive = false);
            item.IsActive = !item.IsActive;
        }

        void OnDragStart(DragDropModel<TItem> item)
        {
            _newPosition.ForEach(x => x.IsActive = false);
            _selected = item;
        }

        void OnDragLeave(DragDropModel<TItem> item)
        {

        }

        void OnDragEnter(DragDropModel<TItem> item)
        {
            if (_selected != null)
            {
                _selected.NewIndex = item.NewIndex > 0 ? item.NewIndex : item.Index;
                _newPosition.Remove(_selected);
                _newPosition.Insert(_selected.NewIndex - 1, _selected);
            }

            int count = 1;
            _newPosition.ForEach(x => x.NewIndex = count++);
        }

        void OnDragEnd()
        {
            if (_selected != null)
            {
                _selected.IsActive = true;
                _newPosition.Remove(_selected);
                _newPosition.Insert(_selected.NewIndex - 1, _selected);
                GenerateNewIndex();
                OnClick(_selected);
                _selected.IsActive = true;
                _selected = null;
            }
        }

        void GenerateNewIndex()
        {
            int count = 1;
            _newPosition.ForEach(x => { x.Index = count++; x.NewIndex = 0; });
        }

        string CheckIfDragOperationIsInProgess()
        {
            return _selected == null ? "" : "dd-inprogess";
        }

        string GetClassForEnterDrag(DragDropModel<TItem> item)
        {
            return _selected != item || _selected == null ? "" : "dd-enter active";
        }
    }
}
