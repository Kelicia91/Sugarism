
namespace Exam
{
    public class StartEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public StartEvent()
        {
            _event = new Handler(onStart);
        }

        // default handler
        private void onStart()
        {
            Log.Debug(string.Format("Exam.onStart;"));
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

    public class EndEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public EndEvent()
        {
            _event = new Handler(onEnd);
        }

        // default handler
        private void onEnd()
        {
            Log.Debug(string.Format("Exam.onEnd;"));
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

    public class DialogueEvent
    {
        public delegate void Handler(string lines);
        private event Handler _event = null;

        public delegate void NPCHandler(int npcId, string lines);
        private event NPCHandler _npcEvent = null;

        public delegate void RivalHandler(Rival rival, string lines);
        private event RivalHandler _rivalEvent = null;

        public DialogueEvent()
        {
            _event = new Handler(onDialogue);
            _npcEvent = new NPCHandler(onDialogueNPC);
            _rivalEvent = new RivalHandler(onDialogueRival);
        }

        // default handler
        private void onDialogue(string lines)
        {
            Log.Debug(string.Format("Exam.onDialogue; {0}", lines));
        }

        public void Invoke(string lines) { _event.Invoke(lines); }

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

        // default handler
        private void onDialogueNPC(int npcId, string lines)
        {
            Log.Debug(string.Format("Exam.onDialogue; npcId({0}): {1}", npcId, lines));
        }

        public void Invoke(int npcId, string lines) { _npcEvent.Invoke(npcId, lines); }

        public void Attach(NPCHandler handler)
        {
            if (null == handler)
                return;

            _npcEvent += handler;
        }

        public void Detach(NPCHandler handler)
        {
            if (null == handler)
                return;

            _npcEvent -= handler;
        }

        // default handler
        private void onDialogueRival(Rival rival, string lines)
        {
            Log.Debug(string.Format("Exam.onDialogue; rival.CharacterId({0}): {1}", rival.characterId, lines));
        }

        public void Invoke(Rival rival, string lines) { _rivalEvent.Invoke(rival, lines); }

        public void Attach(RivalHandler handler)
        {
            if (null == handler)
                return;

            _rivalEvent += handler;
        }

        public void Detach(RivalHandler handler)
        {
            if (null == handler)
                return;

            _rivalEvent -= handler;
        }
    }

}   // namespace