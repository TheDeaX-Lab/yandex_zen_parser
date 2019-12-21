using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Person
    {
        public class Article
        {
            public class FeedbackInfo
            {
                public Boolean is_disliked { get; set; }
                public Boolean is_liked { get; set; }
            }

            public class CardColors
            {
                public String card { get; set; }
                public String text { get; set; }
            }

            public class Stats
            {
                [JsonPropertyName("short")] public String shorti { get; set; }

                public String show { get; set; }
                public String swipe { get; set; }
                public String show_teaser { get; set; }
                public String click_teaser { get; set; }
                public String login_suggest_show { get; set; }
                public String login_suggest_close { get; set; }
                public String auth_click { get; set; }
                public String auth_login { get; set; }
            }

            public class Source
            {
                public class LogoSizes
                {
                    public String framed_100x128_1x { get; set; }
                    public String framed_132x176_1x { get; set; }
                    public String framed_202x260_1x { get; set; }
                    public String framed_212x280_1x { get; set; }
                }

                public String id { get; set; }
                public String strongest_id { get; set; }
                public String title { get; set; }
                public String status { get; set; }
                public String feed_link { get; set; }
                public String feed_share_link { get; set; }
                public String feed_api_link { get; set; }
                public String logo { get; set; }
                public String framed_logo { get; set; }
                public LogoSizes logo_sizes { get; set; }
                public String logo_background_color { get; set; }
                public String title_background_color { get; set; }
                public String title_color { get; set; }
                public String url { get; set; }
                public String type { get; set; }
            }

            public class FeedBack
            {
                public class MoreLessBlock
                {
                    public Boolean main { get; set; }
                    public Boolean delete { get; set; }
                    public String text { get; set; }
                    public String stat { get; set; }
                    public String button_text { get; set; }
                }

                public class Complain
                {
                    public String link { get; set; }
                    public String text { get; set; }
                }

                public MoreLessBlock more { get; set; }
                public MoreLessBlock less { get; set; }
                public MoreLessBlock block { get; set; }
                public MoreLessBlock cancel_more { get; set; }
                public MoreLessBlock cancel_less { get; set; }
                public MoreLessBlock cancel_block { get; set; }

                public Complain complain { get; set; }
            }

            public class Badge
            {
                public String type { get; set; }
                public String text { get; set; }
            }

            public class Video
            {
                public class AdditionalParams
                {
                    public String from { get; set; }
                }

                public String provider { get; set; }
                public String id { get; set; }
                public String player { get; set; }
                public Boolean loop { get; set; }
                public Boolean has_sound { get; set; }
                public List<UInt16> heartbeat_pos { get; set; }
                public Boolean autoplay { get; set; }
                public UInt16 replay_count { get; set; }
                public AdditionalParams additional_params { get; set; }
                public String userAgent { get; set; }
                public String heartbeat { get; set; }
                public String end { get; set; }
            }

            public String type { get; set; }
            public String title { get; set; }
            public String id { get; set; }
            public Boolean read { get; set; }
            public String logo { get; set; }
            public String domain { get; set; }
            public String domain_title { get; set; }
            public String date { get; set; }
            public FeedbackInfo feedback_info { get; set; }
            public Boolean is_favorited { get; set; }
            public Boolean is_promoted { get; set; }
            public Boolean is_promo_publication { get; set; }
            public JsonElement? similar { get; set; }
            public String image { get; set; }
            public String image_squared { get; set; }
            public CardColors card_colors { get; set; }
            public Boolean require_user_data { get; set; }
            public Stats stats { get; set; }
            public String link { get; set; }
            public Source source { get; set; }
            public JsonElement? pixels { get; set; }
            public Boolean notifications { get; set; }
            public FeedBack feedback { get; set; }
            public Video video { get; set; }
            public List<Badge> badges { get; set; }
            public String creation_time { get; set; }
            public UInt64 channel_owner_uid { get; set; }
            public String publisher_id { get; set; }
            public String publication_id { get; set; }
            public String comments_link { get; set; }
            public String all_comments_link { get; set; }
            public String one_comment_link { get; set; }
            public String comments_token { get; set; }
            public String comments_document_id { get; set; }
            public Boolean is_low_resolution_device { get; set; }
            public String text { get; set; }
        }

        public List<Article> items { get; set; }
    }
}