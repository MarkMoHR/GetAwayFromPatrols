using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum SSActionEventType: int { Started, Completed}

public interface ISSActionCallback {
    void SSActionEvent(SSAction source,
        SSActionEventType eventType = SSActionEventType.Completed,
        int intParam = 0, 
        string strParam = null,
        Object objParam = null);
}
