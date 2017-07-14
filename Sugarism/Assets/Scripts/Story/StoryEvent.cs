
namespace Story
{
    public class CmdLinesEvent
    {
        public delegate void Handler(int characterId, bool isAnonymous,
                                    string lines, Sugarism.ELinesEffect linesEffect);
        private event Handler _event = null;

        public CmdLinesEvent()
        {
            _event = new Handler(onCmdLines);
        }

        // default handler
        private void onCmdLines(int characterId, bool isAnonymous,
                                    string lines, Sugarism.ELinesEffect linesEffect)
        {
            Log.Debug(string.Format(
                "onCmdLines; characterId({0}), isAnonymous({1}), linesEffect({2}), lines: \"{3}\"",
                characterId, isAnonymous, linesEffect, lines));
        }

        public void Invoke(int characterId, bool isAnonymous,
                            string lines, Sugarism.ELinesEffect linesEffect)
        {
            _event.Invoke(characterId, isAnonymous, lines, linesEffect);
        }

        // @warn : attach order
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


    public class CmdAppearEvent
    {
        public delegate void Handler(int characterId, Sugarism.EFace face
                                , Sugarism.ECostume costume, Sugarism.EPosition position);
        private event Handler _event = null;

        public CmdAppearEvent()
        {
            _event = new Handler(onCmdAppear);
        }

        // default handler
        private void onCmdAppear(int characterId, Sugarism.EFace face
                                , Sugarism.ECostume costume, Sugarism.EPosition position)
        {
            Log.Debug(string.Format("onCmdAppear; characterId({0}, face({1}), costume({2}), pos({3})",
                        characterId, face, costume, position));
        }

        public void Invoke(int characterId, Sugarism.EFace face
                                , Sugarism.ECostume costume, Sugarism.EPosition position)
        {
            _event.Invoke(characterId, face, costume, position);
        }

        // @warn : attach order
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


    public class CmdSwitchEvent
    {
        public delegate void Handler(CmdCase[] caseArray);
        private event Handler _event = null;

        public CmdSwitchEvent()
        {
            _event = new Handler(onCmdSwitch);
        }

        // default handler
        private void onCmdSwitch(CmdCase[] caseArray)
        {
            int numOfCase = -1;
            if (null == caseArray)
                Log.Error("onCmdSwitch; caseArray is null");
            else if (caseArray.Length <= 0)
                numOfCase = 0;
            else
                numOfCase = caseArray.Length;

            Log.Debug(string.Format("onCmdSwitch; num of case ({0})", numOfCase));
        }

        public void Invoke(CmdCase[] caseArray) { _event.Invoke(caseArray); }

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


    public class CmdTextEvent
    {
        public delegate void Handler(string text);
        private event Handler _event = null;

        public CmdTextEvent()
        {
            _event = new Handler(onCmdText);
        }

        // default handler
        private void onCmdText(string text)
        {
            Log.Debug(string.Format("onCmdText; {0}", text));
        }

        public void Invoke(string text) { _event.Invoke(text); }

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


    public class CmdBackgroundEvent
    {
        public delegate void Handler(int id);
        private event Handler _event = null;

        public CmdBackgroundEvent()
        {
            _event = new Handler(onCmdBackground);
        }

        // default handler
        private void onCmdBackground(int id)
        {
            Log.Debug(string.Format("onCmdBackground; Id({0})", id));
        }

        public void Invoke(int id) { _event.Invoke(id); }

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


    public class CmdMiniPictureEvent
    {
        public delegate void Handler(int id);
        private event Handler _event = null;

        public CmdMiniPictureEvent()
        {
            _event = new Handler(onCmdMiniPicture);
        }

        // default handler
        private void onCmdMiniPicture(int id)
        {
            Log.Debug(string.Format("onCmdMiniPicture; Id({0})", id));
        }

        public void Invoke(int id) { _event.Invoke(id); }

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


    public class CmdPictureEvent
    {
        public delegate void Handler(int id);
        private event Handler _event = null;

        public CmdPictureEvent()
        {
            _event = new Handler(onCmdPicture);
        }

        // default handler
        private void onCmdPicture(int id)
        {
            Log.Debug(string.Format("onCmdPicture; Id({0})", id));
        }

        public void Invoke(int id) { _event.Invoke(id); }

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


    public class CmdSEEvent
    {
        public delegate void Handler(int id);
        private event Handler _event = null;

        public CmdSEEvent()
        {
            _event = new Handler(onCmdSE);
        }

        // default handler
        private void onCmdSE(int id)
        {
            Log.Debug(string.Format("onCmdSE; Id({0})", id));
        }

        public void Invoke(int id) { _event.Invoke(id); }

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


    public class CmdFilterEvent
    {
        public delegate void Handler(Sugarism.EFilter filter);
        private event Handler _event = null;

        public CmdFilterEvent()
        {
            _event = new Handler(onCmdFilter);
        }

        // default handler
        private void onCmdFilter(Sugarism.EFilter filter)
        {
            Log.Debug(string.Format("onCmdFilter; {0}", filter));
        }

        public void Invoke(Sugarism.EFilter filter) { _event.Invoke(filter); }

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


    public class CmdFeelingEvent
    {
        public delegate void Handler(int characterId, Sugarism.EOperation op, int value);
        private event Handler _event = null;

        public CmdFeelingEvent()
        {
            _event = new Handler(onCmdFeeling);
        }

        // default handler
        private void onCmdFeeling(int characterId, Sugarism.EOperation op, int value)
        {
            Log.Debug(string.Format("onCmdFeeling; characterId({0}), op({1}), value({2})",
                    characterId, op, value));
        }

        public void Invoke(int characterId, Sugarism.EOperation op, int value) { _event.Invoke(characterId, op, value); }

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


    public class ScenarioStartEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public ScenarioStartEvent()
        {
            _event = new Handler(onScenarioStart);
        }

        // default handler
        private void onScenarioStart()
        {
            Log.Debug("onScenarioStart");
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


    public class ScenarioEndEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public ScenarioEndEvent()
        {
            _event = new Handler(onScenarioEnd);
        }

        // default handler
        private void onScenarioEnd()
        {
            Log.Debug("onScenarioEnd");
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

}   // namespace