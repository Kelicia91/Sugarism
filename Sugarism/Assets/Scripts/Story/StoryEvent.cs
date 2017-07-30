
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
        public delegate void Handler(int characterId, Sugarism.EPosition position);
        private event Handler _event = null;

        public CmdAppearEvent()
        {
            _event = new Handler(onCmdAppear);
        }

        // default handler
        private void onCmdAppear(int characterId, Sugarism.EPosition position)
        {
            Log.Debug(string.Format("onCmdAppear; characterId({0}), pos({1})",
                        characterId, position));
        }

        public void Invoke(int characterId, Sugarism.EPosition position)
        {
            _event.Invoke(characterId, position);
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
        public delegate void Handler(int targetId, Sugarism.EOperation op, int value);
        private event Handler _event = null;

        public CmdFeelingEvent()
        {
            _event = new Handler(onCmdFeeling);
        }

        // default handler
        private void onCmdFeeling(int targetId, Sugarism.EOperation op, int value)
        {
            Log.Debug(string.Format("onCmdFeeling; targetId({0}), op({1}), value({2})",
                    targetId, op, value));
        }

        public void Invoke(int targetId, Sugarism.EOperation op, int value) { _event.Invoke(targetId, op, value); }

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
    

    public class CmdTargetAppearEvent
    {
        public delegate void Handler(int targetId, bool isBlush, Sugarism.EFace face
                                , Sugarism.ECostume costume, Sugarism.EPosition position);
        private event Handler _event = null;

        public CmdTargetAppearEvent()
        {
            _event = new Handler(onCmdTargetAppear);
        }

        // default handler
        private void onCmdTargetAppear(int targetId, bool isBlush, Sugarism.EFace face
                                , Sugarism.ECostume costume, Sugarism.EPosition position)
        {
            Log.Debug(string.Format("onCmdTargetAppear; targetId({0}, face({1}), blush({2}), costume({3}), pos({4})",
                        targetId, face, isBlush, costume, position));
        }

        public void Invoke(int targetId, bool isBlush, Sugarism.EFace face
                        , Sugarism.ECostume costume, Sugarism.EPosition position)
        {
            _event.Invoke(targetId, isBlush, face, costume, position);
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


    public class CmdDisappearEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public CmdDisappearEvent()
        {
            _event = new Handler(onCmdDisappear);
        }

        // default handler
        private void onCmdDisappear()
        {
            Log.Debug(string.Format("onCmdDisappear;"));
        }

        public void Invoke()
        {
            _event.Invoke();
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


    public class FeelingChangeEvent
    {
        public delegate void Handler(int feeling);
        private event Handler _event = null;

        public FeelingChangeEvent()
        {
            _event = new Handler(onFeelingChanged);
        }

        // default handler
        private void onFeelingChanged(int feeling)
        {
            Log.Debug(string.Format("onFeelingChanged; {0}", feeling));
        }

        public void Invoke(int feeling) { _event.Invoke(feeling); }

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


    public class LastOpenedScenarioNoChangeEvent
    {
        public delegate void Handler(int no);
        private event Handler _event = null;

        public LastOpenedScenarioNoChangeEvent()
        {
            _event = new Handler(onLastOpenedScenarioNoChanged);
        }

        // default handler
        private void onLastOpenedScenarioNoChanged(int no)
        {
            Log.Debug(string.Format("onLastOpenedScenarioNoChanged; {0}", no));
        }

        public void Invoke(int no) { _event.Invoke(no); }

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


    public class SelectTargetEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public SelectTargetEvent()
        {
            _event = new Handler(onSelectTarget);
        }

        // default handler
        private void onSelectTarget()
        {
            Log.Debug("onSelectTarget");
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