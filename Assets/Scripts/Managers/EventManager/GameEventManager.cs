using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : SingletonNoMono<GameEventManager>
{
    private interface IEventface
    {

    }

    private class EventHander:IEventface
    {
        private event Action _action;

        public EventHander(Action action)
        {
            _action = action;
        }

        public void AddCallBack(Action action)
        {
            _action+=action;
        }

        public void RemoveCallBack(Action action)
        {
            _action -=action;
        }

        public void CallBack()
        {
            _action?.Invoke();
        }
    }

    private class EventHander<T> : IEventface
    {
        private event Action<T> _action;

        public EventHander(Action<T> action)
        {
            _action = action;
        }

        public void AddCallBack(Action<T> action)
        {
            _action += action;
        }

        public void RemoveCallBack(Action<T> action)
        {
            _action -= action;
        }

        public void CallBack(T param)
        {
            _action?.Invoke(param);
        }
    }

    private class EventHander<T,T1>:IEventface
    {
        private event Action<T, T1> _action;

        public EventHander(Action<T,T1> action)
        {
            _action = action;
        }

        public void AddCallBack(Action<T,T1> action)
        {
            _action += action;
        }

        public void RemoveCallBack(Action<T, T1> action)
        {
            _action -= action;
        }

        public void CallBack(T param,T1 param1)
        {
            _action?.Invoke(param,param1);
        }
    }


    private Dictionary<string,IEventface> EventCenters = new Dictionary<string,IEventface>();

    public void AddEventListening(string EName,Action action)
    {
        if(EventCenters.TryGetValue(EName,out var e))
        {
            (e as EventHander)?.AddCallBack(action);
        }
        else
        {
            EventCenters.Add(EName, new EventHander(action));
        }
    }


    public void AddEventListening<T>(string EName,Action<T> action)
    {
        if(EventCenters.TryGetValue(EName,out var e))
        {
            (e as EventHander<T>)?.AddCallBack(action);
        }
        else
        {
            EventCenters.Add(EName, new EventHander<T>(action));
        }
    }

    public void AddEventListening<T,T1>(string EName, Action<T, T1> action)
    {
        if (EventCenters.TryGetValue(EName, out var e))
        {
            (e as EventHander<T, T1>)?.AddCallBack(action);
        }
        else
        {
            EventCenters.Add(EName, new EventHander<T, T1>(action));
        }
    }

    public void RemoveEvent(string name,Action action)
    {
        if(EventCenters.TryGetValue(name,out var e))
        {
            (e as EventHander)?.RemoveCallBack(action);
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");
        }
    }

    public void RemoveEvent<T>(string name,Action<T> action)
    {
        if(EventCenters.TryGetValue(name,out var e))
        {
            (e as EventHander<T>)?.RemoveCallBack(action);
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");
        }
    }

    public void RemoveEvent<T,T1>(string name,Action<T,T1> action)
    {
        if(EventCenters.TryGetValue(name,out var e))
        {
            (e as EventHander<T, T1>)?.RemoveCallBack(action);
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");
        }
    }


    public void CallBack(string name)
    {
        if(EventCenters.TryGetValue(name,out var e))
        {
            (e as EventHander)?.CallBack();
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");
        }
    }

    public void CallBack<T>(string name,T param)
    {
        if(EventCenters.TryGetValue(name,out var  e))
        {
            (e as EventHander<T>)?.CallBack(param);
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");

        }
    }

    public void CallBack<T,T1>(string name,T param,T1 param1)
    {
        if (EventCenters.TryGetValue(name, out var e))
        {
            (e as EventHander<T, T1>)?.CallBack(param, param1);
        }
        else
        {
            DevelopmentTool.WTF("Current Evvent does not exist in EventCenters!");
        }
    }


}
