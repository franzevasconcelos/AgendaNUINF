using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;
using Newtonsoft.Json;
using WebView.Models;
using WebView.ViewModels;

namespace WebView.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            try {
                string obj;
                using (var client = new WebClient()) {
                    obj = client.DownloadString(API.Pessoas());
                }

                var pessoasDTO = JsonConvert.DeserializeObject(obj, typeof(List<PessoaDTO>));
                var listaPessoasViewModel = Mapper.Map<List<ListagemViewModel>>(pessoasDTO);

                return View(listaPessoasViewModel);
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new List<ListagemViewModel>());
            }
        }

        [HttpPost]
        public ActionResult Index(PesquisaViewModel viewModel) {
            try {
                string obj;
                using (var client = new WebClient()) {
                    obj = client.DownloadString(API.Pessoas() + $"?nome={viewModel.Nome}&cpf={viewModel.CPF}");
                }

                var pessoasDTO = JsonConvert.DeserializeObject(obj, typeof(List<PessoaDTO>));
                var listaPessoasViewModel = Mapper.Map<List<ListagemViewModel>>(pessoasDTO);

                return PartialView("_Listagem", listaPessoasViewModel);
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new List<ListagemViewModel>());
            }
        }

        public ActionResult Cadastro() {
            return View(new CadastroViewModel {Telefones = new List<TelefoneViewModel>()});
        }

        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel viewModel) {
            if (!ModelState.IsValid)
                return View(viewModel);

            var novaPessoa = Mapper.Map<PessoaDTO>(viewModel);

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(API.Pessoas(), "POST", JsonConvert.SerializeObject(novaPessoa));
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id) {
            try {
                string obj;
                using (var client = new WebClient()) {
                    obj = client.DownloadString(API.Pessoa(id));
                }

                var pessoaDTO = JsonConvert.DeserializeObject(obj, typeof(PessoaDTO));
                var cadastroViewModel = Mapper.Map<CadastroViewModel>(pessoaDTO);

                return View("Cadastro", cadastroViewModel);
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Cadastro");
            }
        }

        [HttpPost]
        public ActionResult Editar(CadastroViewModel viewModel) {
            if (!ModelState.IsValid)
                return View("Cadastro", viewModel);

            var novaPessoa = Mapper.Map<PessoaDTO>(viewModel);

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try {
                    client.UploadString(API.Pessoa(viewModel.Id), "PUT", JsonConvert.SerializeObject(novaPessoa));
                } catch (WebException ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("Cadastro", viewModel);
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Deletar(int id) {
            try {
                using (var client = new WebClient()) {
                    client.UploadString(API.Pessoa(id), "DELETE", String.Empty);
                }

                return RedirectToAction("Index");
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AdicionarTelefone(int idPessoa, TelefoneViewModel viewModel) {
            try {
                using (var client = new WebClient()) {
                    var telefone = Mapper.Map<TelefoneDTO>(viewModel);

                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(API.Telefones(idPessoa), "POST", JsonConvert.SerializeObject(telefone));
                }

                string obj;
                using (var client = new WebClient()) {
                    obj = client.DownloadString(API.Telefones(idPessoa));
                }

                var telefonesDTO = JsonConvert.DeserializeObject(obj, typeof(List<TelefoneDTO>));
                var novaViewModel = Mapper.Map<List<TelefoneViewModel>>(telefonesDTO);
                return PartialView("_Telefones", novaViewModel);
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeletarTelefone(int id, int idPessoa) {
            try {
                using (var client = new WebClient()) {
                    client.UploadString(API.Telefone(id, idPessoa), "DELETE", String.Empty);
                }

                string obj;
                using (var client = new WebClient()) {
                    obj = client.DownloadString(API.Telefones(idPessoa));
                }

                var telefonesDTO = JsonConvert.DeserializeObject(obj, typeof(List<TelefoneDTO>));
                var viewModel = Mapper.Map<List<TelefoneViewModel>>(telefonesDTO);

                return PartialView("_Telefones", viewModel);
            } catch (WebException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}