using System;
using System.Collections.Generic;

namespace Exam.ViTube
{
    public class ViTubeRepository : IViTubeRepository
    {
        private HashSet<User> users;
        private HashSet<Video> videos;
        private Dictionary<User, HashSet<Video>> watchedVideos;
        public ViTubeRepository()
        {
            this.users = new HashSet<User>();
            this.videos = new HashSet<Video>();
            this.watchedVideos = new Dictionary<User, HashSet<Video>>();
        }

        public bool Contains(User user)
        {
           return this.users.Contains(user);
        }

        public bool Contains(Video video)
        {
            return this.videos.Contains(video);
        }

        public void DislikeVideo(User user, Video video)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetPassiveUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByActivityThenByName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetVideos()
        {
            return this.videos;
        }

        public IEnumerable<Video> GetVideosOrderedByViewsThenByLikesThenByDislikes()
        {
            throw new NotImplementedException();
        }

        public void LikeVideo(User user, Video video)
        {
            throw new NotImplementedException();
        }

        public void PostVideo(Video video)
        {
           this.videos.Add(video);
        }

        public void RegisterUser(User user)
        {
            this.users.Add(user);
        }

        public void WatchVideo(User user, Video video)
        {
            if (!this.users.Contains(user) || !this.videos.Contains(video))
            {
                throw new ArgumentException();
            }


        }
    }
}
