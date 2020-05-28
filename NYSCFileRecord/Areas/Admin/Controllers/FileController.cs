﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;
using NYSCFileRecord.Repositories.Services;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _db;

        FileQueries filesValue = new FileQueries();
        FileRecordRepository fileRecordRepository = new FileRecordRepository();
        public FileController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var result = await filesValue.GetAllFiles(_db);

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FileRecord fileRecord)
        {
            if(fileRecord == null)
            {
                throw new ArgumentNullException(nameof(fileRecord));
            }

            if(ModelState.IsValid)
            {
                var result = await fileRecordRepository.SaveFileRecord(_db, fileRecord);

                return RedirectToAction(nameof(Index));
            }

            return PartialView(fileRecord);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if(Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await filesValue.GetFilesById(_db, Id);

            if(result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(FileRecord fileRecord)
        {
            if (fileRecord == null)
            {
                throw new ArgumentNullException(nameof(fileRecord));
            }

            if (ModelState.IsValid)
            {
                var result = await fileRecordRepository.SaveEditFileRecord(_db, fileRecord);

                return RedirectToAction(nameof(Index));
            }

            return PartialView(fileRecord);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await filesValue.GetFilesById(_db, Id);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(FileRecord fileRecord)
        {
            if (fileRecord == null)
            {
                throw new ArgumentNullException(nameof(fileRecord));
            }

            if (ModelState.IsValid)
            {
                var result = await fileRecordRepository.SaveDeleteFileRecord(_db, fileRecord);

                return RedirectToAction(nameof(Index));
            }

            return PartialView(fileRecord);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await filesValue.GetFilesById(_db, Id);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }
    }
}