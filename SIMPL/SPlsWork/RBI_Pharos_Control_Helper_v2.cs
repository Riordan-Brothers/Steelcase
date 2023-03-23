using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_RBI_PHAROS_CONTROL_HELPER_V2
{
    public class UserModuleClass_RBI_PHAROS_CONTROL_HELPER_V2 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput CLEARLVL;
        Crestron.Logos.SplusObjects.DigitalInput STARTPHAROS;
        Crestron.Logos.SplusObjects.DigitalInput STOPPHAROS;
        Crestron.Logos.SplusObjects.DigitalInput PAUSEPHAROS;
        Crestron.Logos.SplusObjects.DigitalInput RESUMEPHAROS;
        Crestron.Logos.SplusObjects.DigitalInput RATECHANGE;
        Crestron.Logos.SplusObjects.DigitalInput POSITIONSET;
        Crestron.Logos.SplusObjects.AnalogInput WHITELVL;
        Crestron.Logos.SplusObjects.AnalogInput REDLVL;
        Crestron.Logos.SplusObjects.AnalogInput GREENLVL;
        Crestron.Logos.SplusObjects.AnalogInput BLUELVL;
        Crestron.Logos.SplusObjects.AnalogInput RATECHANGELVL;
        Crestron.Logos.SplusObjects.AnalogInput POSITIONSETLVL;
        Crestron.Logos.SplusObjects.BufferInput PHAROSRX;
        Crestron.Logos.SplusObjects.DigitalOutput STARTPHAROSFB;
        Crestron.Logos.SplusObjects.DigitalOutput STOPPHAROSFB;
        Crestron.Logos.SplusObjects.DigitalOutput PAUSEPHAROSFB;
        Crestron.Logos.SplusObjects.StringOutput PHAROSTX;
        UShortParameter CONTROLID;
        UShortParameter FADETIME;
        UShortParameter CONTROLTYPEPARAM;
        CrestronString CONTROLTYPE;
        CrestronString STEMPDATA;
        private void SEND_LEVELS (  SplusExecutionContext __context__, ushort WHITE , ushort RED , ushort GREEN , ushort BLUE ) 
            { 
            
            __context__.SourceCodeLine = 39;
            MakeString ( PHAROSTX , "{0}{1:d3},{2:X2},{3:X2},{4:X2},{5:X2},{6:d2}\r", CONTROLTYPE , (ushort)CONTROLID  .Value, WHITE, RED, GREEN, BLUE, (ushort)FADETIME  .Value) ; 
            
            }
            
        private void SEND_CONTROL (  SplusExecutionContext __context__, CrestronString PHAROSCOMMAND ) 
            { 
            
            __context__.SourceCodeLine = 45;
            MakeString ( PHAROSTX , "{0}{1}{2:d3}\r", PHAROSCOMMAND , CONTROLTYPE , (ushort)CONTROLID  .Value) ; 
            
            }
            
        private void PROCESS_FB (  SplusExecutionContext __context__, CrestronString DATA ) 
            { 
            CrestronString STEMP;
            CrestronString SVAL;
            STEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 25, this );
            SVAL  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
            
            
            __context__.SourceCodeLine = 51;
            STEMP  .UpdateValue ( Functions.Remove ( "fb" , DATA )  ) ; 
            
            }
            
        object PHAROSRX_OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 61;
                while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "\r" , PHAROSRX ) > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 63;
                    STEMPDATA  .UpdateValue ( Functions.Gather ( "\r" , PHAROSRX )  ) ; 
                    __context__.SourceCodeLine = 61;
                    }
                
                __context__.SourceCodeLine = 64;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "fb" , STEMPDATA ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 65;
                    PROCESS_FB (  __context__ , STEMPDATA) ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object CLEARLVL_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 76;
            MakeString ( PHAROSTX , "{0}Clear{1:d3},{2:d2}\r", CONTROLTYPE , (ushort)CONTROLID  .Value, (ushort)FADETIME  .Value) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object WHITELVL_OnChange_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 85;
        SEND_LEVELS (  __context__ , (ushort)( WHITELVL  .UshortValue ), (ushort)( REDLVL  .UshortValue ), (ushort)( GREENLVL  .UshortValue ), (ushort)( BLUELVL  .UshortValue )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object REDLVL_OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 93;
        SEND_LEVELS (  __context__ , (ushort)( WHITELVL  .UshortValue ), (ushort)( REDLVL  .UshortValue ), (ushort)( GREENLVL  .UshortValue ), (ushort)( BLUELVL  .UshortValue )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object GREENLVL_OnChange_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 101;
        SEND_LEVELS (  __context__ , (ushort)( WHITELVL  .UshortValue ), (ushort)( REDLVL  .UshortValue ), (ushort)( GREENLVL  .UshortValue ), (ushort)( BLUELVL  .UshortValue )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BLUELVL_OnChange_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 109;
        SEND_LEVELS (  __context__ , (ushort)( WHITELVL  .UshortValue ), (ushort)( REDLVL  .UshortValue ), (ushort)( GREENLVL  .UshortValue ), (ushort)( BLUELVL  .UshortValue )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object STARTPHAROS_OnPush_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 118;
        SEND_CONTROL (  __context__ , "start") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object STOPPHAROS_OnPush_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 123;
        SEND_CONTROL (  __context__ , "stop") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object PAUSEPHAROS_OnPush_8 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 128;
        SEND_CONTROL (  __context__ , "pause") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object RESUMEPHAROS_OnPush_9 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 133;
        SEND_CONTROL (  __context__ , "resume") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 144;
        
            {
            int __SPLS_TMPVAR__SWTCH_1__ = ((int)CONTROLTYPEPARAM  .Value);
            
                { 
                if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 0) ) ) ) 
                    {
                    __context__.SourceCodeLine = 146;
                    MakeString ( CONTROLTYPE , "group") ; 
                    }
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 1) ) ) ) 
                    {
                    __context__.SourceCodeLine = 147;
                    MakeString ( CONTROLTYPE , "fixture") ; 
                    }
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 2) ) ) ) 
                    {
                    __context__.SourceCodeLine = 148;
                    MakeString ( CONTROLTYPE , "TL") ; 
                    }
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 3) ) ) ) 
                    {
                    __context__.SourceCodeLine = 149;
                    MakeString ( CONTROLTYPE , "scene") ; 
                    }
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 2) ) ) ) 
                    {
                    __context__.SourceCodeLine = 150;
                    MakeString ( CONTROLTYPE , "trigger") ; 
                    }
                
                } 
                
            }
            
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    CONTROLTYPE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
    STEMPDATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 255, this );
    
    CLEARLVL = new Crestron.Logos.SplusObjects.DigitalInput( CLEARLVL__DigitalInput__, this );
    m_DigitalInputList.Add( CLEARLVL__DigitalInput__, CLEARLVL );
    
    STARTPHAROS = new Crestron.Logos.SplusObjects.DigitalInput( STARTPHAROS__DigitalInput__, this );
    m_DigitalInputList.Add( STARTPHAROS__DigitalInput__, STARTPHAROS );
    
    STOPPHAROS = new Crestron.Logos.SplusObjects.DigitalInput( STOPPHAROS__DigitalInput__, this );
    m_DigitalInputList.Add( STOPPHAROS__DigitalInput__, STOPPHAROS );
    
    PAUSEPHAROS = new Crestron.Logos.SplusObjects.DigitalInput( PAUSEPHAROS__DigitalInput__, this );
    m_DigitalInputList.Add( PAUSEPHAROS__DigitalInput__, PAUSEPHAROS );
    
    RESUMEPHAROS = new Crestron.Logos.SplusObjects.DigitalInput( RESUMEPHAROS__DigitalInput__, this );
    m_DigitalInputList.Add( RESUMEPHAROS__DigitalInput__, RESUMEPHAROS );
    
    RATECHANGE = new Crestron.Logos.SplusObjects.DigitalInput( RATECHANGE__DigitalInput__, this );
    m_DigitalInputList.Add( RATECHANGE__DigitalInput__, RATECHANGE );
    
    POSITIONSET = new Crestron.Logos.SplusObjects.DigitalInput( POSITIONSET__DigitalInput__, this );
    m_DigitalInputList.Add( POSITIONSET__DigitalInput__, POSITIONSET );
    
    STARTPHAROSFB = new Crestron.Logos.SplusObjects.DigitalOutput( STARTPHAROSFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( STARTPHAROSFB__DigitalOutput__, STARTPHAROSFB );
    
    STOPPHAROSFB = new Crestron.Logos.SplusObjects.DigitalOutput( STOPPHAROSFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( STOPPHAROSFB__DigitalOutput__, STOPPHAROSFB );
    
    PAUSEPHAROSFB = new Crestron.Logos.SplusObjects.DigitalOutput( PAUSEPHAROSFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( PAUSEPHAROSFB__DigitalOutput__, PAUSEPHAROSFB );
    
    WHITELVL = new Crestron.Logos.SplusObjects.AnalogInput( WHITELVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( WHITELVL__AnalogSerialInput__, WHITELVL );
    
    REDLVL = new Crestron.Logos.SplusObjects.AnalogInput( REDLVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( REDLVL__AnalogSerialInput__, REDLVL );
    
    GREENLVL = new Crestron.Logos.SplusObjects.AnalogInput( GREENLVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( GREENLVL__AnalogSerialInput__, GREENLVL );
    
    BLUELVL = new Crestron.Logos.SplusObjects.AnalogInput( BLUELVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( BLUELVL__AnalogSerialInput__, BLUELVL );
    
    RATECHANGELVL = new Crestron.Logos.SplusObjects.AnalogInput( RATECHANGELVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( RATECHANGELVL__AnalogSerialInput__, RATECHANGELVL );
    
    POSITIONSETLVL = new Crestron.Logos.SplusObjects.AnalogInput( POSITIONSETLVL__AnalogSerialInput__, this );
    m_AnalogInputList.Add( POSITIONSETLVL__AnalogSerialInput__, POSITIONSETLVL );
    
    PHAROSTX = new Crestron.Logos.SplusObjects.StringOutput( PHAROSTX__AnalogSerialOutput__, this );
    m_StringOutputList.Add( PHAROSTX__AnalogSerialOutput__, PHAROSTX );
    
    PHAROSRX = new Crestron.Logos.SplusObjects.BufferInput( PHAROSRX__AnalogSerialInput__, 2000, this );
    m_StringInputList.Add( PHAROSRX__AnalogSerialInput__, PHAROSRX );
    
    CONTROLID = new UShortParameter( CONTROLID__Parameter__, this );
    m_ParameterList.Add( CONTROLID__Parameter__, CONTROLID );
    
    FADETIME = new UShortParameter( FADETIME__Parameter__, this );
    m_ParameterList.Add( FADETIME__Parameter__, FADETIME );
    
    CONTROLTYPEPARAM = new UShortParameter( CONTROLTYPEPARAM__Parameter__, this );
    m_ParameterList.Add( CONTROLTYPEPARAM__Parameter__, CONTROLTYPEPARAM );
    
    
    PHAROSRX.OnSerialChange.Add( new InputChangeHandlerWrapper( PHAROSRX_OnChange_0, true ) );
    CLEARLVL.OnDigitalPush.Add( new InputChangeHandlerWrapper( CLEARLVL_OnPush_1, false ) );
    WHITELVL.OnAnalogChange.Add( new InputChangeHandlerWrapper( WHITELVL_OnChange_2, true ) );
    REDLVL.OnAnalogChange.Add( new InputChangeHandlerWrapper( REDLVL_OnChange_3, true ) );
    GREENLVL.OnAnalogChange.Add( new InputChangeHandlerWrapper( GREENLVL_OnChange_4, true ) );
    BLUELVL.OnAnalogChange.Add( new InputChangeHandlerWrapper( BLUELVL_OnChange_5, true ) );
    STARTPHAROS.OnDigitalPush.Add( new InputChangeHandlerWrapper( STARTPHAROS_OnPush_6, false ) );
    STOPPHAROS.OnDigitalPush.Add( new InputChangeHandlerWrapper( STOPPHAROS_OnPush_7, false ) );
    PAUSEPHAROS.OnDigitalPush.Add( new InputChangeHandlerWrapper( PAUSEPHAROS_OnPush_8, false ) );
    RESUMEPHAROS.OnDigitalPush.Add( new InputChangeHandlerWrapper( RESUMEPHAROS_OnPush_9, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_RBI_PHAROS_CONTROL_HELPER_V2 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint CLEARLVL__DigitalInput__ = 0;
const uint STARTPHAROS__DigitalInput__ = 1;
const uint STOPPHAROS__DigitalInput__ = 2;
const uint PAUSEPHAROS__DigitalInput__ = 3;
const uint RESUMEPHAROS__DigitalInput__ = 4;
const uint RATECHANGE__DigitalInput__ = 5;
const uint POSITIONSET__DigitalInput__ = 6;
const uint WHITELVL__AnalogSerialInput__ = 0;
const uint REDLVL__AnalogSerialInput__ = 1;
const uint GREENLVL__AnalogSerialInput__ = 2;
const uint BLUELVL__AnalogSerialInput__ = 3;
const uint RATECHANGELVL__AnalogSerialInput__ = 4;
const uint POSITIONSETLVL__AnalogSerialInput__ = 5;
const uint PHAROSRX__AnalogSerialInput__ = 6;
const uint STARTPHAROSFB__DigitalOutput__ = 0;
const uint STOPPHAROSFB__DigitalOutput__ = 1;
const uint PAUSEPHAROSFB__DigitalOutput__ = 2;
const uint PHAROSTX__AnalogSerialOutput__ = 0;
const uint CONTROLID__Parameter__ = 10;
const uint FADETIME__Parameter__ = 11;
const uint CONTROLTYPEPARAM__Parameter__ = 12;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
