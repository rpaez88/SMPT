namespace SMPT.Client.Core
{
    public class ComponentEvents:MessaginCenter
    {
        public event StateNotify? Event;

        public void EventEmited(object sender, StateArguments args)
        {
            if (Event != null)
            {
                Event(sender, args);
            }
        }
    }
}
