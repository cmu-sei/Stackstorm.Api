// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;

namespace Stackstorm.Api.Client.Test
{
    public class Core : StackstormBase 
    {
        //TODO
        /*UUID
         *executionResult = await client.Core.Uuid(null);

                            id = executionResult.id ?? throw new Exception();
                            isComplete = executionResult.IsComplete();
                            
                            while (!isComplete)
                            {
                                executionResult = await client.Executions.GetExecutionAsync(id);
                                isComplete = executionResult.IsComplete();
                                Console.WriteLine(executionResult.status);
                                Thread.Sleep(2000);
                            }

                            Console.WriteLine($"{id} completed as {executionResult.status} with a result of {executionResult.result}");
                            break;
         * 
         */
    }
}
