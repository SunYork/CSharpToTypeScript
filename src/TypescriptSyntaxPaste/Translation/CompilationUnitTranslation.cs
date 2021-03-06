﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace TypescriptSyntaxPaste.Translation
{
    public class CompilationUnitTranslation : SyntaxTranslation
    {
        private SemanticModel semanticModel;

        public new CompilationUnitSyntax Syntax
        {
            get { return (CompilationUnitSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SyntaxListTranslation<MemberDeclarationSyntax, MemberDeclarationTranslation> Members { get; set; }

        public CompilationUnitTranslation(CompilationUnitSyntax syntax, SyntaxTranslation parent) : base(syntax, null)
        {
            Members = syntax.Members.Get<MemberDeclarationSyntax, MemberDeclarationTranslation>(this);
        }

        public Solution Solution { get; set; }

        public Compilation Compilation { get; set; }

        public List<GotoStatementTranslation> GotoLabeledStatements { get; set; } = new List<GotoStatementTranslation>();
        public List<ContinueStatementTranslation> ContinueStatements { get; set; } = new List<ContinueStatementTranslation>();

        public SemanticModel SemanticModel
        {
            get
            {
                if (semanticModel == null)
                {
                    semanticModel = Compilation.GetSemanticModel(Syntax.SyntaxTree);
                }

                return semanticModel;
            }
            set
            {
                semanticModel = value;
            }
        }

        protected override string InnerTranslate()
        {
            return Members.Translate();
        }
    }
}
