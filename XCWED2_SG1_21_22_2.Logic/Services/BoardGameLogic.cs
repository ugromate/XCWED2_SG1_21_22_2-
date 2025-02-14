﻿using System;
using System.Collections.Generic;
using System.Linq;
using XCWED2_SG1_21_22_2.Logic.Interfaces;
using XCWED2_SG1_21_22_2.Models.Entities;
using XCWED2_SG1_21_22_2.Models.Models;
using XCWED2_SG1_21_22_2.Repository.Interfaces;

namespace XCWED2_SG1_21_22_2.Logic.Services
{
    public class BoardGameLogic : IBoardGameLogic
    {
        IBoardGameRepository _boardGameRepository;
        IPublisherRepository _publisherRepository;
        IDesignerRepository _designerRepository;

        public BoardGameLogic(IBoardGameRepository boardGameRepository, IPublisherRepository publisherRepository, IDesignerRepository designerRepository)
        {
            _boardGameRepository = boardGameRepository;
            _publisherRepository = publisherRepository;
            _designerRepository = designerRepository;
        }

        public BoardGame Create(BoardGame entity)
        {

            //if (entity == null)
            //{
            //    throw new Exception();
            //}

            if (entity.DesignerID == 0)
            {
                throw new ApplicationException("Please choose Designer ID");
            }

            if (entity.PublisherID == 0)
            {
                throw new ApplicationException("Please choose Publisher ID");
            }

            if (String.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ApplicationException("Name is required!");
            }

            if (entity.PriceHUF < 0)
            {
                throw new ApplicationException("Price must be greater or equal to 0!");
            }  

            if (entity.MinAge < 0)
            {
                throw new ApplicationException("Minimum age must be greater or equal to 0!");
            }       
            
            if (entity.MinPlayer < 1)
            {
                throw new ApplicationException("Minimum player must be greater or equal to 1!");
            }  
            
            if (entity.MaxPlayer < entity.MinPlayer)
            {
                throw new ApplicationException("Maximum player must be greater or equal to minimum player!");
            }

            if (entity.Rating < 0)
            {
                throw new ApplicationException("Rating must be greater or equal to 0!");
            }

            var result = _boardGameRepository.Create(entity);

            return result;
        }
        public BoardGame Read(int id)
        {
            return _boardGameRepository.Read(id);
        }

        public IList<BoardGame> ReadAll()
        {
            return _boardGameRepository.ReadAll().ToList();
        }

        public BoardGame Update(BoardGame entity)
        {

            //if (entity == null)
            //{
            //    throw new Exception();
            //}

            if (entity.DesignerID == 0)
            {
                throw new ApplicationException("Please choose Designer ID");
            }

            if (entity.PublisherID == 0)
            {
                throw new ApplicationException("Please choose Publisher ID");
            }

            if (String.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ApplicationException("Name is required!");
            }

            if (entity.PriceHUF < 0)
            {
                throw new ApplicationException("Price must be greater or equal to 0!");
            }

            if (entity.MinAge < 0)
            {
                throw new ApplicationException("Minimum age must be greater or equal to 0!");
            }

            if (entity.MinPlayer < 1)
            {
                throw new ApplicationException("Minimum player must be greater or equal to 1!");
            }

            if (entity.MaxPlayer < entity.MinPlayer)
            {
                throw new ApplicationException("Maximum player must be greater or equal to minimum player!");
            }

            if (entity.Rating < 0)
            {
                throw new ApplicationException("Rating must be greater or equal to 0!");
            }

            var result = _boardGameRepository.Update(entity);

            return result;
        }
        public void Delete(int id)
        {
            _boardGameRepository.Delete(id);
        }

        public int TwoKidGameCount()
        {
            var games = from boardgame in _boardGameRepository.ReadAll()
                        where boardgame.MinAge < 11 && boardgame.MinPlayer == 2
                        select new
                        {
                            Name = boardgame.Name
                        };
            return games.Count();
        }

        public BoardGame BestAlonePlayable()
        {
            var games = from boardgame in _boardGameRepository.ReadAll()
                        where boardgame.MinPlayer == 1
                        select boardgame;

            var ordered = games.OrderByDescending(x => x.Rating);

            return (ordered.FirstOrDefault());
        }

        public IEnumerable<NationalityCount> GamesByDesignerNationality()
        {
            var countryList = from boardgame in _boardGameRepository.ReadAll()
                              join designer in _designerRepository.ReadAll()
                              on boardgame.DesignerID equals designer.Id
                              group boardgame by designer.Nationality into grouped
                              select new NationalityCount()
                              {
                                  Nationality = grouped.Key,
                                  Count = grouped.Count()
                              };

            var list = countryList.ToList().OrderByDescending(x => x.Count).ThenBy(x => x.Nationality);

            return list.ToList();
        }

    }
}
