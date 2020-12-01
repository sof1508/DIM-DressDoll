using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace DressDoll
{
    public partial class Form1 : Form
    {
     private System.Speech.Recognition.SpeechRecognitionEngine _recognizer =
        new SpeechRecognitionEngine();
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        /* VARIABLES GLOBALES */
  

        public Form1()
        {
            InitializeComponent();
            InicializarImagenes();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la Aplicación");

            Grammar grammar = CreateGrammarBuilderDollDressSemantics(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            // Nivel de confianza del reconocimiento 70%
            _recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 50);
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            //reconocimiento asíncrono y múltiples veces
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synth.Speak("Aplicación preparada para reconocer su voz");
        }


                void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
                      //obtenemos un diccionario con los elementos semánticos
                      SemanticValue semantics = e.Result.Semantics;
          
                      string rawText = e.Result.Text;
                      RecognitionResult result = e.Result;
            
                      if (!semantics.ContainsKey("partesArriba") && !semantics.ContainsKey("partesAbajo"))
                      {
                            synth.Speak("Lo siento no te entiendo");
                      }
                      else
                      {
                            synth.Speak("hola estoy funcionando");

                            if (semantics.ContainsKey("partesAbajo")) {
                                 synth.Speak("hola he entrado en falda");
                                 parteAbajo.Image = DressDoll.Properties.Resources.Summer_Skirt;
                        }
                          
                      }
        }
        

        private Grammar CreateGrammarBuilderDollDressSemantics(params int[] info)
        {
            //synth.Speak("Creando ahora la gramática");
            Choices partesAbajoChoice = new Choices();
            Choices partesArribaChoice = new Choices();
            Choices parteEnteraChoice = new Choices();
            Choices calzadoChoice = new Choices();


            // Partes de abajo 

            SemanticResultValue choiceResultValue =
                    new SemanticResultValue("Falda", "Summer_Skirt");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Pantalón", "Autumn Pants");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            // Fin partes de abajo


            // Partes de arriba 

            new SemanticResultValue("Camiseta", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            new SemanticResultValue("Chaqueta", "Spring Jacket");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            new SemanticResultValue("Abrigo", "Autumn Coat");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);


            // Fin partes de arriba

            // Partes enteras

            new SemanticResultValue("Vestido", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

           
            // Fin partes enteras

            SemanticResultKey choiceResultKeyArriba = new SemanticResultKey("partesArriba", partesArribaChoice);
            SemanticResultKey choiceResultKeyAbajo = new SemanticResultKey("partesAbajo", partesAbajoChoice );
            GrammarBuilder ropa = new GrammarBuilder(choiceResultKeyAbajo);


            GrammarBuilder poner = "Ponerse";
            GrammarBuilder quitar = "Quitarse";
            GrammarBuilder ir = "Ir";
            GrammarBuilder cambiar = "Cambiar";

            Choices alternativas = new Choices(poner, quitar, cambiar, ir);
          

            GrammarBuilder frase = new GrammarBuilder(alternativas);

           
            frase.Append(ropa);
            Grammar grammar = new Grammar(frase);
            grammar.Name = "Poner/Cambiar";

            //Grammar grammar = new Grammar("so.xml.txt");

            return grammar;



        }

    }
}
