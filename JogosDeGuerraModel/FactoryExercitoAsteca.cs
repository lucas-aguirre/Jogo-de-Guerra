﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class FactoryExercitoAsteca : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroAsteca();
        }

        public override Cavaleiro CriarCavalaria()
        {
            return new CavaleiroAsteca();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroAsteca();
        }
    }
}
