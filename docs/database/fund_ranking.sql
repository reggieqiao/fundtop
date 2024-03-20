CREATE TABLE `poplar`.`fund_ranking`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `fund_code` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `fund_shortname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `date` date NULL DEFAULT NULL,
  `net_asset_value` decimal(8, 4) NULL DEFAULT NULL,
  `cumulative_net_asset_value` decimal(8, 4) NULL DEFAULT NULL,
  `daily_growth_rate` decimal(8, 2) NULL DEFAULT NULL,
  `one_week` decimal(8, 2) NULL DEFAULT NULL,
  `one_month` decimal(8, 2) NULL DEFAULT NULL,
  `three_month` decimal(8, 2) NULL DEFAULT NULL,
  `six_month` decimal(8, 2) NULL DEFAULT NULL,
  `one_year` decimal(8, 2) NULL DEFAULT NULL,
  `two_year` decimal(8, 2) NULL DEFAULT NULL,
  `three_year` decimal(8, 2) NULL DEFAULT NULL,
  `year_to_date` decimal(8, 2) NULL DEFAULT NULL,
  `since_inception` decimal(8, 2) NULL DEFAULT NULL,
  `inception_date` date NULL DEFAULT NULL,
  `transaction_fee` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `created_at` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
);