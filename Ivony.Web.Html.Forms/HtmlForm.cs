﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivony.Fluent;
using System.Collections;

namespace Ivony.Web.Html.Forms
{
  public class HtmlForm
  {
    private IHtmlElement _element;

    public IHtmlElement Element
    {
      get { return _element; }
    }

    public HtmlForm( IHtmlElement element )
    {
      _element = element;

      Initialize();

    }


    public IEnumerable<IHtmlInput> InputControls
    {
      get { return inputTexts.Cast<IHtmlInput>().Union( inputGroups.Cast<IHtmlInput>() ); }
    }


    /// <summary>
    /// 所有文本输入控件
    /// </summary>
    public HtmlInputText[] TextInputs
    {
      get { return inputTexts; }
    }

    /// <summary>
    /// 所有输入控件组
    /// </summary>
    public IHtmlInputGroup[] GroupInputs
    {
      get { return inputGroups; }
    }


    private HtmlInputText[] inputTexts;
    private IHtmlInputGroup[] inputGroups;
    private HtmlLabel[] labels;

    private Hashtable labelsTable = Hashtable.Synchronized( new Hashtable() );


    private void Initialize()
    {
      inputTexts = Element.Find( "input[type=text]", "input[type=password]", "input[type=hidden]", "textarea" )
          .Select( element => new HtmlInputText( this, element ) ).ToArray(); ;


      inputGroups = Element.Find( "select" ).Select( select => new HtmlSelect( this, select ) ).Cast<IHtmlInputGroup>()
        .Union( HtmlButtonGroup.CaptureInputGroups( this ).Cast<IHtmlInputGroup>() ).ToArray();

      labels = Element.Find( "label" ).Select( element => new HtmlLabel( this, element ) ).ToArray();

      labels.GroupBy( l => l.BindElement ).ForAll( grouping =>
        labelsTable.Add( grouping.Key, grouping.ToArray() ) );

    }


    /// <summary>
    /// 检索指定控件的 Label
    /// </summary>
    /// <param name="element">要检索 Label 的控件</param>
    /// <returns></returns>
    public HtmlLabel[] FindLabels( IHtmlElement element )
    {
      return (HtmlLabel[]) labelsTable[element];
    }

  }
}
