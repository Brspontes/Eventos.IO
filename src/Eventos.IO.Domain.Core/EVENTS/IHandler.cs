﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.EVENTS
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}
