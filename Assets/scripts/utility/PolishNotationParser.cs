using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PolishNotationParser : MonoBehaviour
{

  public TMP_InputField output;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test(string arg){
      try{
        string outs = arg + " = " + Parse(arg).ToString();
        output.text = outs;
      }
      catch(Exception ex){
        output.text = ex.Message;
      }
    }

    public decimal Parse(string expression){
      string[] args = expression.Split(' ');
      Stack<decimal> stack = new Stack<decimal>();
      decimal number = decimal.Zero;
      foreach (string token in args){
        if (decimal.TryParse(token,out number)){
          stack.Push(number);
        }
        else{
          switch(token){
            case "^":
              {
                number = stack.Pop();
                stack.Push((decimal)Math.Pow((double)stack.Pop(),(double)number));
                break;
              }
            case "root":
              {
                number = stack.Pop();
                stack.Push((decimal)Math.Pow((double)stack.Pop(),1.0/(double)number));
                break;
              }
            case "sqrt": //Вот эта будет точнее 2 x root
              {
                number = stack.Pop();
                stack.Push((decimal)Math.Sqrt((double)number));
                break;
              }
            case "*":
              {
                  stack.Push(stack.Pop() * stack.Pop());
                  break;
              }
            case "/":
              {
                  number = stack.Pop();
                  stack.Push(stack.Pop() / number);
                  break;
              }
            case "+":
              {
                  stack.Push(stack.Pop() + stack.Pop());
                  break;
              }
            case "-":
              {
                  number = stack.Pop();
                  stack.Push(stack.Pop() - number);
                  break;
              }
              case "%":
                {
                    number = stack.Pop();
                    stack.Push(stack.Pop() % number);
                    break;
                }
            default:
              throw new FormatException("Failed to interpret expression");
          }
        }
      }
      return stack.Pop();
    }

}
