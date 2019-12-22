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

        public class Auth
        {
            public Boolean is_authorized{ get; set; }
        }

        public class Exp
        {
            public String rezen_no_tablet{ get; set; }
            public String desktop_rtb_ab{ get; set; }
            public String tykva_enabled{ get; set; }
            public String new_experiments_service{ get; set; }
            [JsonPropertyName("default")]
            public String defaulti { get; set; }
        }

        public class ExperimentsData
        {
            [JsonExtensionData] public IDictionary<string, JsonElement> extra { get; set; }
        }

        public class GridType
        {
            public String name{ get; set; }
            public UInt16 column_count{ get; set; }
            public Double portion_max_size{ get; set; }
            public UInt16 portion_multiplicity{ get; set; }
            public List<UInt16> small_positions{ get; set; }
            public List<UInt16> big_positions{ get; set; }
            public List<UInt16> grid_positions{ get; set; }
        }

        public class ClientLogs
        {
            public String link{ get; set; }
        }

        public class Channel
        {
            public class Source
            {
                public class LogoSizes
                {
                    public String framed_100x128_1x{ get; set; }
                    public String framed_132x176_1x{ get; set; }
                    public String framed_202x260_1x{ get; set; }
                    public String framed_212x280_1x{ get; set; }
                }

                public String id{ get; set; }
                public String strongest_id{ get; set; }
                public String title{ get; set; }
                public String status{ get; set; }
                public String feed_link{ get; set; }
                public String feed_share_link{ get; set; }
                public String feed_api_link{ get; set; }
                public String logo{ get; set; }
                public String framed_logo{ get; set; }
                public LogoSizes logo_sizes{ get; set; }
                public String logo_background_color{ get; set; }
                public String title_background_color{ get; set; }
                public String title_color{ get; set; }
                public String og_image{ get; set; }
                public String subtitle{ get; set; }
                public String description{ get; set; }
                public UInt32 subscribers{ get; set; }
                public List<JsonElement?> social_links{ get; set; }
                public UInt32 audience{ get; set; }
                public Boolean is_indexable{ get; set; }
                public String type{ get; set; }
                
            }

            public class StatEvents
            {
                public String feedback_favourite{ get; set; }
                public String feedback_cancel_favourite{ get; set; }
                public String feedback_block{ get; set; }
                public String feedback_cancel_block{ get; set; }
                public String complain_show{ get; set; }
                public String complain_click{ get; set; }
                public String share_click{ get; set; }
                public String login_suggest_show{ get; set; }
                public String login_suggest_close{ get; set; }
                public String auth_click{ get; set; }
                public String auth_login{ get; set; }
            }

            public String status{ get; set; }
            public Source source{ get; set; }
            public String og_image{ get; set; }
            public Boolean is_indexable{ get; set; }
            public StatEvents stat_events{ get; set; }
            public String bulk_params{ get; set; }
        }

        public class More
        {
            public String link{ get; set; }
        }

        public class User
        {
            public class LastSawFeed
            {
                public JsonElement? zen_app{ get; set; }
                public JsonElement? zen_app_ios{ get; set; }
            }

            public LastSawFeed last_saw_feed { get; set; }
        }

        public class Links
        {
            public String all_channels_link { get; set; }
        }

        public List<Article> items { get; set; }
        public String rid{ get; set; }
        public Auth auth{ get; set; }
        public UInt16 ttl{ get; set; }
        public UInt32 store_ttl{ get; set; }
        public Boolean have_zen{ get; set; }
        public Boolean show_zen{ get; set; }
        public Exp exp{ get; set; }
        public ExperimentsData experiments_data{ get; set; }
        public UInt64 generate_time{ get; set; }
        public Boolean ice_start{ get; set; }
        public GridType grid_type{ get; set; }
        public ClientLogs client_logs{ get; set; }
        public String platform_status{ get; set; }
        public Channel channel{ get; set; }
        public More more{ get; set; }
        public List<JsonElement?> precache_resources{ get; set; }
        public String user_status_on_publishers_platform{ get; set; }
        public User user{ get; set; }
        public Links links{ get; set; }
        public String group_ids{ get; set; }
    }
}