using BlazorWebAppCamilla.Models;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;

namespace BlazorWebAppCamilla.Services;

public class CourseService(GraphQLHttpClient client)
{
    private readonly GraphQLHttpClient _client = client;

    public async Task<Course> GetCourseByIdAsync(string id)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query ($id: String!) {
                getCourseById(id: $id) {
                    id
                    title
                    imageUri
                    imageHeaderUri
                    isBestseller
                    isDigital
                    categories
                    ingress
                    starRating
                    reviews
                    likes
                    likesInProcent
                    hours
                    authors {
                        name
                        authorImage
                    }
                    prices {
                        currency
                        price
                        discount
                    }
                    content {
                        description
                        includes
                        programDetails {
                            id
                            title
                            description
                        }
                    }
                }
            }",
            Variables = new { id }
        };

        var response = await _client.SendQueryAsync<CourseResponse>(request);
        return response.Data.GetCourseById;
    }

    public async Task<IEnumerable<string>> GetUserCourseIdsAsync(string userId)
    {
        var request = new GraphQLRequest
        {
            Query = @"
        query GetUserCourseIds($userId: String!) {
            getUserCourseIds(userId: $userId)
        }",
            Variables = new { userId }
        };

        var response = await _client.SendQueryAsync<UserCoursesQueryResponse>(request);
        return response.Data.GetUserCourseIds;
    }

    public async Task<UserCourses> RequestCreateUserCoursesAsync(UserCourses userCourses)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            mutation ($input: CreateUserCourseInput!) {
                saveUserCourse(input: $input) {
                    userId
                    courseId
                }
            }",
            Variables = new
            {
                input = new
                {
                    userId = userCourses.UserId,
                    courseId = userCourses.CourseId
                }
            }
        };

        var response = await _client.SendMutationAsync<UserCourses>(request);
        return response.Data;
    }

    public async Task<bool> RequestRemoveAllUserCourseAsync(string userId)
    {
        var request = new GraphQLRequest
        {
            Query = @"
        mutation deleteAllUserCourses($userId: String!) {
            deleteAllUserCourses(userId: $userId)
        }",
            Variables = new { userId }
        };

        var response = await _client.SendMutationAsync<DeleteUserCourseResponse>(request);
        return response.Data.deleteAllUserCourses;
    }

    public async Task<bool> RequestRemoveUserCourseAsync(UserCourses userCourse)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            mutation deleteUserCourse($userCourse: UserCoursesInput!) {
                deleteUserCourse(userCourse: $userCourse)
            }",
            Variables = new
            {
                userCourse = new
                {
                    UserId = userCourse.UserId,
                    CourseId = userCourse.CourseId
                }
            }
        };

        var response = await _client.SendMutationAsync<DeleteUserCourseResponse>(request);
        return response.Data.deleteUserCourse;
    }

    private class DeleteUserCourseResponse
    {
        public bool deleteUserCourse { get; set; }
        public bool deleteAllUserCourses { get; set; } 
    }

    private class CourseResponse
    {
        public Course GetCourseById { get; set; }
    }

    private class UserCoursesQueryResponse
    {
        [JsonProperty("getUserCourseIds")]
        public IEnumerable<string> GetUserCourseIds { get; set; }
    }

}
