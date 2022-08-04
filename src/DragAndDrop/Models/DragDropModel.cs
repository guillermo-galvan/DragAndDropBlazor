namespace DragAndDrop.Models
{
    public class DragDropModel<T>
    {
        public int Index { get; set; }

        public T? Item { get; set; }

        public int NewIndex { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"Index: {Index} IndexNew: {NewIndex}";
        }
    }
}
