    using CL2.Models;
using System;
using System.Linq;

namespace CL2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CL2Context context)
        {
            context.Database.EnsureCreated();

            //Orden oficial de creación de tablas:
            //I CAPA DE DATOS
            //    - Administraciones
            //    - Legislativos
            //    - Temas
            //    - Diputados
            //    - Fracciones
            //    - Plenarios
            //    - Comisiones
            //    - Proyectos de Ley
            //II CAPA DE DATOS
            //    - Proyecto Tema
            //    - Proyecto Diputado
            //    - Integrantes Plenario
            //    - Comision Legislativo
            //    - Comision Diputado
            //III CAPA DE DATOS
            //    - TIPO ACTIVIDAD PLENARIO
            //    - SESION PLENARIO
            //    - CONTROL POLITICO
            //    - SESION PLENARIO ACTIVIDAD
            //    - DISCUSION PLENARIO

            //    - TIPO ACTIVIDAD COMISION
            //    - SESION COMISION
            //    - SESION COMISION ACTIVIDAD
            //    - DISCUSION COMISION
              

             // --------------- I CAPA DE DATOS ------------------------------------------------------------   

            // Administraciones
            if (context.Administraciones.Any())
            {
                return;   // DB has been seeded
            }

            var administraciones = new Administracion[]
            {
                new Administracion{AdministracionPeriodo="2018-2022",PresidenteRepublica="Carlos Alvarado"},
                new Administracion{AdministracionPeriodo="2022-2026",PresidenteRepublica="Rodrigo Cháves"}
            };

            foreach (Administracion a in administraciones)
            {
                context.Administraciones.Add(a);
            }
            context.SaveChanges();

            //Legislativos

            if (context.Legislativos.Any())
            {
                return;   // DB has been seeded
            }
            var legislativos = new Legislativo[]
            {
                new Legislativo{AnnoLegislativo="2018-2019", AdministracionID=1},
                new Legislativo{AnnoLegislativo="2019-2020", AdministracionID=1},
                new Legislativo{AnnoLegislativo="2020-2021", AdministracionID=1},
                new Legislativo{AnnoLegislativo="2021-2022", AdministracionID=1},
                new Legislativo{AnnoLegislativo="2022-2023", AdministracionID=2},
                new Legislativo{AnnoLegislativo="2023-2024", AdministracionID=2},
                new Legislativo{AnnoLegislativo="2024-2025", AdministracionID=2},
                new Legislativo{AnnoLegislativo="2025-2026", AdministracionID=2}
            };
            foreach (Legislativo l in legislativos)
            {
                context.Legislativos.Add(l);
            }
            context.SaveChanges();

            //Temas

            if (context.Temas.Any())
            {
                return;   // DB has been seeded
            }
            var temas = new Tema[]
            {
                new Tema{TemaDesc="Economía"},
                new Tema{TemaDesc="Ambiente"}
            };
            foreach (Tema t in temas)
            {
                context.Temas.Add(t);
            }
            context.SaveChanges();

            ////Inserts de Diputado
            //if (context.Diputados.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var diputados = new Diputado[]
            //{
            //    new Diputado{NombreDiputado="Pilar", PrimerApellido="Cisnero", SegundoApellido="Gallo", Provincia="San Jose", CorreoDiputado="pilar.cisneroseda@asamblea.go.cr",
            //        TelefonoDiputado="12345678", GeneroDiputado="F", Cedula="800490390", FechaNacimiento= DateTime.Parse("1954-06-23")},
            //    new Diputado{NombreDiputado="Fabricio", PrimerApellido="Alvarado", SegundoApellido="Muñoz", Provincia="San Jose", CorreoDiputado="​fabricio.alvarado@asamblea.go.cr",
            //        TelefonoDiputado="12345678", GeneroDiputado="M", Cedula="108820284", FechaNacimiento= DateTime.Parse("1974-05-30")},
            //    new Diputado{NombreDiputado="Eli", PrimerApellido="Feinzaig", SegundoApellido="Mintz", Provincia="San Jose", CorreoDiputado="eliecer.feinzaig@asamblea.go.cr",
            //        TelefonoDiputado="12345678", GeneroDiputado="F", Cedula="106520768", FechaNacimiento= DateTime.Parse("1965-03-23")}
            //};

            //foreach (Diputado d in diputados)
            //{
            //    context.Diputados.Add(d);
            //}
            //context.SaveChanges();

            //inserts de fracciones

            if (context.Fracciones.Any())
            {
                return;   // DB has been seeded
            }
            var fracc = new Fraccion[]
            {
                new Fraccion{NombreFraccion="Partido Acción Ciudadana", SiglasFraccion="PAC"},
                new Fraccion{NombreFraccion="Frente Amplio", SiglasFraccion="FA"}
            };
            foreach (Fraccion t in fracc)
            {
                context.Fracciones.Add(t);
            }
            context.SaveChanges();//*/

            //Inserts de Plenario
            if (context.Plenarios.Any())
            {
                return;   // DB has been seeded
            }
            var plenario = new Plenario[]
            {
                new Plenario{AdministracionID=1}
            };
            foreach (Plenario ppp in plenario)
            {
                context.Plenarios.Add(ppp);
            }
            context.SaveChanges();

            //Inserts de Comision
            if (context.Comisiones.Any())
            {
                return; // DB has been seeded
            }

            var comisiones = new Comision[]
            {
                new Comision{NombreComision="De Honores", Detalle="Declaratoria de Benemeritos", Tipo="Permanente Especial", TemaID=1},
                new Comision{NombreComision="De Nombramientos", Detalle="Ratificacion de la ARESEP", Tipo="Permanente Especial", TemaID=2},
                new Comision{NombreComision="Plena Segunda", Detalle="Ministro Edgar Ayales, Ministerio de Hacienda", Tipo="Plena Segunda", TemaID=2},
                new Comision{NombreComision="De Gobierno y Administracion", Detalle="Reforma a Ley de Empleo Publico", Tipo="Permanente Ordinario", TemaID=1}
            };

            foreach (Comision c in comisiones)
            {
                context.Comisiones.Add(c);
            }
            context.SaveChanges();

            //Inserts de Proyectos de Ley
            if (context.ProyectoLeys.Any())
            {
                return;   // DB has been seeded
            }

            var proyectoley = new ProyectoLey[]
            {
                new ProyectoLey{NumeroExpediente=23175, TituloCorto="Enajenar un bien inmueble demanial registrado a su nombre",
                    TituloCompleto="AUTORIZACIÓN AL PODER JUDICIAL PARA ENAJENAR UN BIEN INMUEBLE DEMANIAL REGISTRADO A SU NOMBRE",
                    FechaPresentacion= DateTime.Parse("2022-06-15"), CantidadFirmas=13}
            };

            foreach (ProyectoLey pl in proyectoley)
            {
                context.ProyectoLeys.Add(pl);
            }
            context.SaveChanges();

            // --------------- II CAPA DE DATOS ------------------------------------------------------------

            //Inserts de Proyecto Temas
            if (context.ProyectoTemas.Any())
            {
                return;   // DB has been seeded
            }
            var proyectotema = new ProyectoTema[]
            {
                new ProyectoTema{ProyectoLeyID=1, TemaID=1}
            };
            foreach (ProyectoTema pt in proyectotema)
            {
                context.ProyectoTemas.Add(pt);
            }
            context.SaveChanges();

            //Inserts de Proyecto Diputado
            if (context.ProyectoDiputados.Any())
            {
                return;   // DB has been seeded
            }
            var proyectodiputado = new ProyectoDiputado[]
            {
                new ProyectoDiputado{ProyectoLeyID=1, DiputadoID=1}
            };
            foreach (ProyectoDiputado pd in proyectodiputado)
            {
                context.ProyectoDiputados.Add(pd);
            }
            context.SaveChanges();



            //Inserts de Integrantes de Plenario
            if (context.IntegrantesPlenarios.Any())
            {
                return;   // DB has been seeded
            }
            var integrantes = new IntegrantesPlenario[]
            {
                new IntegrantesPlenario{DiputadoID=1, PlenarioID=1, Detalle_Puesto="Presidencia"},
            };
            foreach (IntegrantesPlenario ip in integrantes)
            {
                context.IntegrantesPlenarios.Add(ip);
            }
            context.SaveChanges();

            //Inserts de Comision Legislativo
            if (context.ComisionLegislativo.Any())
            {
                return;   // DB has been seeded
            }
            var comisLegis = new ComisionLegislativo[]
            {
                new ComisionLegislativo{ComisionID=1, LegislativoID=1},
            };
            foreach (ComisionLegislativo ip in comisLegis)
            {
                context.ComisionLegislativo.Add(ip);
            }
            context.SaveChanges();

            //Inserts de Comision Diputado
            if (context.ComisionDiputados.Any())
            {
                return;   // DB has been seeded
            }
            var comisDipu = new ComisionDiputados[]
            {
                new ComisionDiputados{ComisionLegislativoID=1, DiputadoID=1, DetallePuesto="Presidencia"},
            };
            foreach (ComisionDiputados ip in comisDipu)
            {
                context.ComisionDiputados.Add(ip);
            }
            context.SaveChanges();

            // --------------- III CAPA DE DATOS ------------------------------------------------------------


            //Inserts de SesionPlenario
            if (context.SesionPlenarios.Any())
            {
                return; // DB has been seeded
            }

            var sesionplenario = new SesionPlenario[]
            {
                new SesionPlenario{PlenarioID=1, NumeroSesion=1, PeriodoSesionPlenario="Ordinario", TipoSesionPlenario="Ordinaria", FechaSesionPlenario=DateTime.Parse("1954-06-23")}
            };

            foreach (SesionPlenario c in sesionplenario)
            {
                context.SesionPlenarios.Add(c);
            }
            context.SaveChanges();

            ////Inserts de Control Politico
            if (context.ControlPoliticos.Any())
            {
                return; // DB has been seeded
            }

            var control = new ControlPolitico[]
            {
               new ControlPolitico{SesionPlenarioID=1, DiputadoID=1, TemaID=1, ResumenComentario="blahblah", DetalleComentario="blahablalahba"}
            };

            foreach (ControlPolitico c in control)
            {
                context.ControlPoliticos.Add(c);
            }
            context.SaveChanges();

            
            ////Inserts de Sesion Plenario Actividad
            if (context.ComentarioPlenarios.Any())
            {
                return; // DB has been seeded
            }

            var plenarioActividad = new ComentarioPlenario[]
            {
               new ComentarioPlenario{SesionPlenarioID=1, TipoActividad="Segundo Debate", ProyectoLeyID=1, DiputadoID=1, PosicionComentario="A favor", ResumenComentario="Intencion de aprobar el proyecto definitivamente", 
                   DetalleComentario = "El diputado se sento a explicar su apoyo por el proyecto de forma que se pueda aprobar prontamente"}
            };

            foreach (ComentarioPlenario c in plenarioActividad)
            {
                context.ComentarioPlenarios.Add(c);
            }
            context.SaveChanges();



            // Inserts de TipoActividadPlenario
            if (context.TipoActividadComisiones.Any())
            {
                return;   // DB has been seeded
            }
            var tipoActividadComision = new TipoActividadComision[]
            {
                new TipoActividadComision{DetalleActividadComision="Moción"},
                new TipoActividadComision{DetalleActividadComision="Debate"}
            };
            foreach (TipoActividadComision t in tipoActividadComision)
            {
                context.TipoActividadComisiones.Add(t);
            }
            context.SaveChanges();//*/

            //Inserts de Sesion Comision
            if (context.SesionComision.Any())
            {
                return;   // DB has been seeded
            }
            var sesionComision = new SesionComision[]
            {
                new SesionComision{ComisionLegislativoID=1, NumeroSesionComision="1", PeriodoSesionComision="Ordinaria", TipoSesionComision="Extraordinario", FechaSesionComision=DateTime.Today}
            };
            foreach (SesionComision sc in sesionComision)
            {
                context.SesionComision.Add(sc);
            }
            context.SaveChanges();

            //Inserts de Sesion Comision
            if (context.SesionComisionActividad.Any())
            {
                return;   // DB has been seeded
            }
            var comisionActividad = new SesionComisionActividad[]
            {
                new SesionComisionActividad{SesionComisionID=1, TipoActividadID=1, ProyectoLeyID=1, DiputadoID=1, NumeroMocion="1A", TipoMocion="Audiencia", DecisionMocion="Aprobada", VotosContraDictamen=0, VotosFavorDictamen=0, DetalleActividad="Una mocion importante."}
            };
            foreach (SesionComisionActividad sc in comisionActividad)
            {
                context.SesionComisionActividad.Add(sc);
            }
            context.SaveChanges();


            ////Inserts de Discusion Comision
            if (context.DiscusionComision.Any())
            {
                return; // DB has been seeded
            }

            var discusionComision = new DiscusionComision[]
            {
               new DiscusionComision{SesionComisionID=1, SesionComisionActividadID=1, DiputadoID=1, ResumenComentario="Hablaron de esto",
                   DetalleComentario="Importanteeee", TipoComentario="Positivo"}
            };

            foreach (DiscusionComision c in discusionComision)
            {
                context.DiscusionComision.Add(c);
            }
            context.SaveChanges();


        }
    }
}
