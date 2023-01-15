﻿using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class SongRepository: ISongRepository
	{
		private AppDbContext db;

		public SongRepository()
		{
			db = new AppDbContext();
		}

		public List<SongIndexVM> FindAll()
		{
			return db.Songs
				.Include("Song_Artist_Metadata")
				.Include("SongGenre")
				.Select(x => new
				{
					x.id,
					x.songName,
					artistList = x.Song_Artist_Metadata.Select(m => m.Artist).Select(a => a.artistName).ToList(),
					genreName = x.SongGenre.genreName,
					x.language,
					x.released,
					x.duration,
					x.songWriter,
					x.songPath,
				}).ToList()
			.Select(p => new SongIndexVM
			{
				id = p.id,
				songName = p.songName,
				artistList = p.artistList,
				genreName = p.genreName,
				language = p.language,
				released = p.released,
				duration = p.duration,
				songWriter = p.songWriter,
				songPath = "/Uploads/Songs/" + p.songPath,
			}).ToList();
		}

		public void AddNewSong(SongDTO dto)
		{
			db.Songs.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void EditSong(SongDTO dto)
		{
			var song = db.Songs.Find(dto.id);
			db.Entry(song).State = System.Data.Entity.EntityState.Detached;
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
		}

		public Song Search(SongDTO dto)
		{
			Song model;
			if (dto.id == 0)
			{
				model = db.Songs.SingleOrDefault(x => x.songName == dto.songName && x.genreId == dto.genreId && x.duration == dto.duration);
			}
			else
			{
				model = db.Songs.SingleOrDefault(x => x.id != dto.id && x.songName == dto.songName && x.genreId == dto.genreId && x.duration == dto.duration);
			}


			return model;
		}

		public SongEditVM FindById(int id)
		{
			return db.Songs.Include("Song_Artist_Metadata").Select(x => new SongEditVM()
			{
				id = x.id,
				songName = x.songName,
				artistIdList = x.Song_Artist_Metadata.Where(m => m.songId == id).Select(a => a.artistId).ToList(),
				genreId = x.genreId,
				duration = x.duration,
				isInstrumental = x.isInstrumental,
				language = x.language,
				isExplicit = x.isExplicit,
				released = x.released,
				songWriter = x.songWriter,
				lyric = x.lyric,
				songCoverPath = x.songCoverPath,
				songPath = x.songPath,
				status = x.status,
				timesOfPlay = x.timesOfPlay,
			}).SingleOrDefault(x => x.id == id);
		}

		public void DeleteSong(SongDTO dto)
		{
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Deleted;
			db.SaveChanges();
		}

		public IEnumerable<SongListItemVM> GetSongList(int artistId)
		{
			return db.Song_Artist_Metadata.Include("Song").Where(m => m.artistId == artistId).Select(m => new
			{
				m.songId,
				m.Song.songName
			}).Select(x => new SongListItemVM
			{
				songId = x.songId,
				songName = x.songName
			});
		}
	}
}