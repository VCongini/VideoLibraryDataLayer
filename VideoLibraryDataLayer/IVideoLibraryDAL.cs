using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCSD.VideoLibrary
{
    public interface IVideoLibraryDAL
    {
        /// <summary>
        /// Search Videos table using the following rules:
        ///    a. if Title is not null or empty string, search by Title (partial match)
        ///    b. if Director is not null or empty string, search by Director (partial match)
        ///    c. if Year is positive, search by Year (full match)
        ///    d. if more than one condition applies, use AND logic
        ///    e. do not return videos marked as deleted
        ///    f. if no search criteria is provided, return all videos
        ///    g. if search criteria cannot be satisfied, return empty collection
        /// </summary>
        /// <returns>Collection containing search results</returns>
        Collection<VideoSearchResult> SearchVideoLibrary(VideoSearchCriteria criteria);
        
        /// <summary>
        /// Create new row in Checkouts table using the following rules:
        ///     a. throw exception if user already has this video checked out
        ///     b. throw exception if TotalCopies value is 0
        ///     c. throw exception if videoId does not exist
        ///     d. throw exception if userId does not exist
        ///     e. successful checkout should decrement TotalCopies value 
        ///     f. throw exception if video is deleted
        /// </summary>
        void CheckOutVideo(int videoId, Guid userId);
        
        /// <summary>
        /// Update row in Checkouts table that corresponds to the last checkout using the following rules:
        ///     a. successful check-in should increment TotalCopies value
        ///     b. throw exception if corresponding checkout is not found
        ///     c. throw exception if videoId does not exist
        ///     d. throw exception if userId does not exist
        /// </summary>
        void CheckInVideo(int videoId, Guid userId);
       
        /// <summary>
        /// Add new row to the Reviews table using the following rules:
        ///     a. user can create multiple reviews
        ///     b. throw exception if videoId does not exist
        ///     c. throw exception if userId does not exist
        ///     d. throw exception if reviewText is null or empty
        ///     e. throw exception if video is deleted
        /// </summary>
        /// <returns>Integer representing ReviewID</returns>
        int AddReview(int videoId, Guid userId, string reviewText);

        /// <summary>
        /// Update existing row in the Reviews table using the following rules:
        ///     a. throw exception if reviewId does not exist
        ///     b. throw exception if reviewText is null or empty
        /// </summary>
        void UpdateReview(int reviewId, string reviewText);
        
        /// <summary>
        /// Add new or update existing row in the Ratings table using the following rules:
        ///     a. user can only have one rating for a video
        ///     b. throw exception if videoId does not exist
        ///     c. throw exception if userId does not exist
        ///     d. throw exception if rating is less than 1 or greater than 5
        /// </summary>
        void AddUpdateRating(int videoId, Guid userId, int rating);
        
        /// <summary>
        /// Add new or update existing row in the Videos table using the following rules:
        ///     a. create new record if VideoId property value is 0
        ///     b. update existing record if VideoId property value is positive
        ///     c. throw exception if user is not a member of the Administrator role
        ///     d. throw exception if user does not exist
        ///     e. throw exception if VideoId is positive but video does not exist
        ///     f. throw exception if Title, Year, Director values are missing
        ///     g. throw exception if TotalCopies value is negative
        /// </summary>
        /// <returns>Integer representing VideoID</returns>
        int AddUpdateVideo(VideoInfo video, Guid userId);

        /// <summary>
        /// Mark video as deleted by setting IsDeleted column value to 1 using the following rules:
        ///     a. throw exception if video has pending checkouts
        ///     b. throw exception if user does not exist
        ///     c. throw exception if user is not a member of the Administrator role
        ///     d. throw exception if video does not exist
        /// </summary>
        void DeleteVideo(int videoId, Guid userId);
    }
}
