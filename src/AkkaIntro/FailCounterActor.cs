﻿using Akka.Actor;
using System;

namespace AkkaIntro
{
    public class FailCounterActor : ReceiveActor
    {
        private const int MAX = 5;

        private int value;

        public FailCounterActor()
        {
            Receive<Count>(message => Handle(message));
            Receive<GetValue.Request>(message => Handle(message));
        }

        private void Handle(Count message)
        {
            value++;
            if (value >= MAX)
                throw new ArgumentOutOfRangeException(nameof(value));

            PrintCurrentValue();
        }

        private void Handle(GetValue.Request message)
        {
            Sender.Tell(new GetValue.Response(value));
        }

        private void PrintCurrentValue()
        {
            Console.WriteLine($"Counter #{Self.Path.Name}: {value}");
        }
    }
}
