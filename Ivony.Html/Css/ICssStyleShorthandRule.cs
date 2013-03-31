﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ivony.Html
{

  /// <summary>
  /// 定义 CSS 样式设置缩写规则
  /// </summary>
  public interface ICssStyleShorthandRule
  {

    /// <summary>
    /// 属性名
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 解出属性值
    /// </summary>
    /// <param name="value">属性值缩写形式</param>
    /// <returns>解出的属性值</returns>
    CssStyleProperty[] ExtractProperties( string shorthand );

  }


  public class PaddingShorthandRule : ICssStyleShorthandRule
  {
    public string Name
    {
      get { return "padding"; }
    }



    public CssStyleProperty[] ExtractProperties( string shorthand )
    {
      var values = CssStyleHelper.whitespaceRegex.Split( shorthand );

      return CssStyleHelper.GenerateBoxProperties( Name, values );
    }
  }

  public class MarginShorthandRule : ICssStyleShorthandRule
  {
    public string Name
    {
      get { return "margin"; }
    }


    public CssStyleProperty[] ExtractProperties( string shorthand )
    {
      var values = CssStyleHelper.whitespaceRegex.Split( shorthand );

      return CssStyleHelper.GenerateBoxProperties( Name, values );
    }
  }


  public class BorderWidthShorthandRule : ICssStyleShorthandRule
  {
    public string Name
    {
      get { return "border-width"; }
    }

    public CssStyleProperty[] ExtractProperties( string shorthand )
    {
      var values = CssStyleHelper.whitespaceRegex.Split( shorthand );

      return CssStyleHelper.GenerateBoxProperties( Name, values );
    }
  }

  public class BorderStyleShorthandRule : ICssStyleShorthandRule
  {
    public string Name
    {
      get { return "border-style"; }
    }

    public CssStyleProperty[] ExtractProperties( string shorthand )
    {
      var values = CssStyleHelper.whitespaceRegex.Split( shorthand );

      return CssStyleHelper.GenerateBoxProperties( Name, values );
    }
  }

  public class BorderColorShorthandRule : ICssStyleShorthandRule
  {
    public string Name
    {
      get { return "border-color"; }
    }

    public CssStyleProperty[] ExtractProperties( string shorthand )
    {
      var values = CssStyleHelper.whitespaceRegex.Split( shorthand );

      return CssStyleHelper.GenerateBoxProperties( Name, values );
    }
  }




  internal static class CssStyleHelper
  {

    public static readonly Regex whitespaceRegex = new Regex( @"\s+", RegexOptions.Compiled );
    public static CssStyleProperty[] GenerateBoxProperties( string prefix, string[] values )
    {
      string top, right, bottom, left;

      if ( values.Length == 0 )
        return new CssStyleProperty[0];

      top = right = bottom = left = values[0];

      if ( values.Length >= 2 )
        right = left = values[1];

      if ( values.Length >= 3 )
        bottom = values[2];

      if ( values.Length >= 4 )
        left = values[3];


      return new[] { new CssStyleProperty( prefix + "-top", top ), new CssStyleProperty( prefix + "-right", right ), new CssStyleProperty( prefix + "-bottom", bottom ), new CssStyleProperty( prefix + "-left", left ) };
    }


    public static readonly Regex lengthValueRegex = new Regex( @"^\d+(px|cm|in)$" );

    public static bool IsLengthValue( string value )
    {
      return lengthValueRegex.IsMatch( value );
    }

    public static readonly Regex colorValueRegex = new Regex( @"^(#([0-9a-fA-F]{3}|[0-9a-fA-F]{6}))$" );

    public static bool IsLengthValue( string value )
    {
      return colorValueRegex.IsMatch( value );
    }

  }
}