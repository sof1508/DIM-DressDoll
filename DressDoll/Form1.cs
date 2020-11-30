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

        public Form1()
        {
            InitializeComponent();
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
            /*
                      if (!semantics.ContainsKey("rgb"))
                      {
                          this.label1.Text = "No info provided.";
                      }
                      else
                      {
                          this.label1.Text = rawText;
                          this.BackColor = Color.FromArgb((int)semantics["rgb"].Value);
                          Update();
                          //synth.Speak(rawText);
                      }*/
        }
        

        private Grammar CreateGrammarBuilderDollDressSemantics(params int[] info)
        {
            //synth.Speak("Creando ahora la gramática");
            Choices clothChoice = new Choices();


            SemanticResultValue choiceResultValue =
                    new SemanticResultValue("Falda", "Skirt.png");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            clothChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Pantalón", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            clothChoice.Add(resultValueBuilder);


            SemanticResultKey choiceResultKey = new SemanticResultKey("partesArriba", clothChoice);
            SemanticResultKey choiceResultKey = new SemanticResultKey("partesAbajo", clothChoice);
            GrammarBuilder ropa = new GrammarBuilder(choiceResultKey);


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
