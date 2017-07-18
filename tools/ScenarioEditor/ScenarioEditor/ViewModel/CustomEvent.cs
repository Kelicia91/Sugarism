
namespace ScenarioEditor.ViewModel
{
    public class CharacterListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public CharacterListChangeEvent()
        {
            _event = new Handler(onCharacterListChanged);
        }

        // default handler
        private void onCharacterListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }


    public class TargetListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public TargetListChangeEvent()
        {
            _event = new Handler(onTargetListChanged);
        }

        // default handler
        private void onTargetListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }


    public class BackgroundListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public BackgroundListChangeEvent()
        {
            _event = new Handler(onBackgroundListChanged);
        }
        
        // default handler
        private void onBackgroundListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }


    public class MiniPictureListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public MiniPictureListChangeEvent()
        {
            _event = new Handler(onMiniPictureListChanged);
        }

        // default handler
        private void onMiniPictureListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }


    public class PictureListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public PictureListChangeEvent()
        {
            _event = new Handler(onPictureListChanged);
        }

        // default handler
        private void onPictureListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }


    public class SEListChangeEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public SEListChangeEvent()
        {
            _event = new Handler(onSEListChanged);
        }

        // default handler
        private void onSEListChanged()
        {
            // do nothing
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }
}
