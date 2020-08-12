using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{  
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypeController1
        public ActionResult Index()
        {
            var LeaveTypes = _repo.FindAll().ToList();
            var Model = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(LeaveTypes);

            return View(Model);
        }

        // GET: LeaveTypeController1/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeViewModel>(leaveType);
            return View(model);
        }

        // GET: LeaveTypeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;

                var isSuccess = _repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong..");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
        }

        LeaveTypeViewModel GetModel(int id)
        {
            var leaveType = _repo.FindById(id);
            return _mapper.Map<LeaveTypeViewModel>(leaveType);
        }

        // GET: LeaveTypeController1/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            //var leaveType = _repo.FindById(id);
            //var model = _mapper.Map<LeaveTypeViewModel>(leaveType);
            return View(GetModel(id));
        }

        // POST: LeaveTypeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Edit went wrong..");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Edit went wrong..");
                return View(model);
            }
        }

        // GET: LeaveTypeController1/Delete/5
        public ActionResult Delete(int id)
        {
            //if (!_repo.isExists(id))
            //{
            //    return NotFound();
            //}

            //return View(GetModel(id));
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var isSuccess = _repo.Delete(leaveType);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Delete went wrong..");
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LeaveTypeViewModel model)
        {
            try
            {
                var leaveType = _repo.FindById(id);
                if (leaveType == null)
                {
                    return NotFound();
                }

                var isSuccess = _repo.Delete(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Delete went wrong..");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }   
            catch
            {
                return View(model);
            }
        }
    }
}
